using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Archiwum
{
    public partial class FormMain : Form
    {
        BindingSource bSrc = new BindingSource();
        Boolean? naMakulature = null;
        public FormMain()
        {
            InitializeComponent();

            Directory.CreateDirectory(@"bin\backups");

            try
            {
                string path = ConfigurationManager.AppSettings["path"];
                Process.Start(path);
            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            SwConn();

        }

        private void SwConn()
        {
            string cs = ConfigurationManager.AppSettings["cs"];
            try
            {
                cs = string.Format(cs, "root", "root");
                GlobalData.connection = new MySqlConnection(cs);
                GlobalData.connection.Open();

            }
            catch (Exception exc)
            {
                DialogResult result = MessageBox.Show(exc.Message, "Błąd połączenia z serwerem", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Cancel) return;
                else SwConn();
            }

            lblStatus.Text = "połączono z bazą danych";
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void RefreshGrid(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT `index` AS id, lp, symbol_wykaz_akt, tytul, dat_pocz, dat_konc, ltomow, uwagi, dodat_info, dskrajne FROM archiwum.archiwum" + " ");
            sb.Append(condition);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = new MySqlCommand(sb.ToString(), GlobalData.connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            bSrc.DataSource = dt;
            grid.DataSource = bSrc;
            lblStatus.Text = "Ładowanie zakończone";
        }

        private void menu_Click(object sender, EventArgs e)
        {
            switch (sender.ToString())
            {
                case "Archiwum":
                    lblStatus.Text = "Ładuję...";
                    naMakulature = false;
                    RefreshGrid("WHERE ADDDATE(dat_konc, INTERVAL lat_waz YEAR) > CURRENT_DATE OR dat_konc IS NULL");
                    Headers();
                    break;
                case "Na makulaturę":
                    lblStatus.Text = "Ładuję...";
                    naMakulature = true;
                    RefreshGrid("WHERE ADDDATE(dat_konc, INTERVAL lat_waz YEAR) < CURRENT_DATE");
                    Headers();
                    break;
                case "&Pomoc...":
                    FormHelp form = new FormHelp();
                    form.Show();
                    break;
                default:
                    MessageBox.Show("Coś się wypierdutnęło", "Ups..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void LblStatus_MouseHover(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }
        private void Headers()
        {
            grid.Columns["lp"].HeaderText = "Numer szafy";
            grid.Columns["symbol_wykaz_akt"].HeaderText = "Symbol z wykazu akt";
            grid.Columns["tytul"].HeaderText = "Tytuł teczki";
            grid.Columns["dat_pocz"].HeaderText = "Data początkowa";
            grid.Columns["dat_konc"].HeaderText = "Data końcowa";
            grid.Columns["ltomow"].HeaderText = "liczba tomów";
            grid.Columns["uwagi"].HeaderText = "Uwagi";
            grid.Columns["dodat_info"].HeaderText = "Dodatkowe informacje";
            grid.Columns["id"].Visible = false;
            grid.Columns["dskrajne"].Visible = false;

        }

        private void TbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            string input = tbSearch.Text;
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                string searchInput = tbSearch.Text.ToString();
                string a = $"LIKE CONCAT('%', '{searchInput}' ,'%')";
                string b = "AND ADDDATE(dat_konc, INTERVAL lat_waz YEAR) < CURRENT_DATE";
                string c = "AND ADDDATE(dat_konc, INTERVAL lat_waz YEAR) > CURRENT_DATE";
                string d = "AND dat_konc IS NULL";
                switch (naMakulature)
                {
                    case null:
                        break;
                    case true:
                        RefreshGrid($@"WHERE lp {a} {b} OR symbol_wykaz_akt {a} {b} OR tytul {a} {b} OR dat_pocz {a} {b} OR dat_konc {a} {b} OR ltomow {a} {b} OR uwagi {a} {b}");
                        break;
                    case false:
                        RefreshGrid($@"WHERE lp {a} {c} OR lp {a} {d} OR symbol_wykaz_akt {a} {c} OR symbol_wykaz_akt {a} {d} OR tytul {a} {c} OR tytul {a} {d} OR dat_pocz {a} {c}
                        OR dat_pocz {a} {d} OR dat_konc {a} {c} OR ltomow {a} {c} OR ltomow {a} {d} OR uwagi {a} {c} OR uwagi {a} {d}");
                        break;
                    default:
                        MessageBox.Show("Coś się wypierdutnęło", "Ups..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

            }
        }

        private void Menu_Opening(object sender, CancelEventArgs e)
        {
            tbSearch.Text = "";
            if (naMakulature == false || naMakulature == null)
            {
                eksportujDoExcelToolStripMenuItem.Visible = true;
                delAll.Visible = false;
                delAllDiv.Visible = false;
                if (naMakulature == null)
                {
                    eksportujDoExcelToolStripMenuItem.Visible = false;
                }
            }
            else
            {
                eksportujDoExcelToolStripMenuItem.Visible = true;
                delAll.Visible = true;
                delAllDiv.Visible = true;
            }
        }


        private void EdytujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0) return;

            lblStatus.Text = null;
            FormEdit form = new FormEdit();
            int selectedIndex = grid.SelectedRows[0].Index;

            GlobalData.ID = Convert.ToInt32(grid["id", selectedIndex].Value);
            GlobalData.lp = Convert.ToInt32(grid["lp", selectedIndex].Value);
            GlobalData.sWa = grid["symbol_wykaz_akt", selectedIndex].Value.ToString();
            GlobalData.tyt = grid["tytul", selectedIndex].Value.ToString();
            GlobalData.dat_pocz = grid["dat_pocz", selectedIndex].Value.ToString();
            GlobalData.dat_konc = grid["dat_konc", selectedIndex].Value.ToString();
            GlobalData.ltomow = Convert.ToInt32(grid["ltomow", selectedIndex].Value);
            GlobalData.uwagi = grid["uwagi", selectedIndex].Value.ToString();
            GlobalData.dodat_info = grid["dodat_info", selectedIndex].Value.ToString();

            form.ShowDialog();
            lblStatus.Text = GlobalData.Stat;
            GlobalData.Stat = null;
        }

        private void DodajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblStatus.Text = null;
            FormAdd form = new FormAdd();
            form.ShowDialog();
            lblStatus.Text = GlobalData.Stat;
            GlobalData.Stat = null;
        }

        private void UsuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0) return;

            DialogResult result = MessageBox.Show("Czy napewno chcesz usunąć ZAZNACZONY zapis?", "Pytanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;



            string sql = "DELETE FROM archiwum.archiwum WHERE `index` = @id";

            int selecedIndex = grid.SelectedRows[0].Index;
            int id = Convert.ToInt32(grid["id", selecedIndex].Value);

            using (MySqlCommand delete = new MySqlCommand(sql, GlobalData.connection))
            {
                delete.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                delete.ExecuteNonQuery();
            }
            backup();
            grid.Rows.RemoveAt(selecedIndex);
        }

        private void OdświeżToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (naMakulature != null)
            {
                switch (naMakulature)
                {
                    case true:
                        RefreshGrid("WHERE ADDDATE(dat_konc, INTERVAL lat_waz YEAR) < CURRENT_DATE");
                        lblStatus.Text = "Odświeżono";
                        break;
                    case false:
                        RefreshGrid("WHERE ADDDATE(dat_konc, INTERVAL lat_waz YEAR) > CURRENT_DATE OR dat_konc IS NULL");
                        lblStatus.Text = "Odświeżono";
                        break;
                    default:
                        break;
                }
            }
        }

        private void EksportujDoExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application xcel = new Microsoft.Office.Interop.Excel.Application();
                xcel.Application.Workbooks.Add(Type.Missing);
                switch (naMakulature)
                {

                    case true:
                        
                        xcel.Cells[10, 1] = "symbol z wykazu akt";
                        xcel.Cells[10, 2] = "tytuł teczki";
                        xcel.Cells[10, 3] = "daty skrajne";
                        xcel.Cells[10, 4] = "liczba tomów";
                        xcel.Cells[10, 5] = "uwagi";

                        int howManyTimes = grid.Rows.Count / 26;
                        if (howManyTimes == 0) howManyTimes++;
                        if (grid.Rows.Count % 26 != 0) howManyTimes++;

                        xcel.Range["A1", "E" + (grid.Rows.Count + howManyTimes * 10 + 1).ToString()].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                        xcel.Range["A5", "A5"].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                        xcel.Range["A5", "A5"].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDot;

                        xcel.Range[$"B6", $"C6"].Merge();
                        xcel.Cells[6, 2] = "Spis dokumentacji niearchiwalnej (aktowej)";

                        xcel.Range[$"B7", $"C7"].Merge();
                        xcel.Cells[7, 2] = "przeznaczonej na makulaturę lub zniszczenie";

                        xcel.Range[$"B6", $"C7"].Font.Bold = true; ;

                        //upiększanie
                        int borA = 26;
                        for (int i = 10; i < grid.Rows.Count + 1 + howManyTimes * 10; i++)
                        {
                            xcel.Range[$"A{i}", $"A{i}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                            xcel.Range[$"B{i}", $"B{i}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                            xcel.Range[$"C{i}", $"C{i}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                            xcel.Range[$"D{i}", $"D{i}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                            xcel.Range[$"E{i}", $"E{i}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                            if (i == borA + 10)
                            {
                                i += 10;
                                borA += 37;
                            }
                        }

                        //wypełnainie
                        borA = 26;
                        int x = 11;
                        for (int i = 0; i < grid.Rows.Count; i++)
                        {
                            xcel.Cells[x, 1] = grid.Rows[i].Cells["symbol_wykaz_akt"].Value.ToString();
                            xcel.Cells[x, 2] = grid.Rows[i].Cells["tytul"].Value.ToString();
                            xcel.Cells[x, 3] = grid.Rows[i].Cells["dskrajne"].Value.ToString();
                            xcel.Cells[x, 4] = grid.Rows[i].Cells["ltomow"].Value.ToString();
                            xcel.Cells[x, 5] = grid.Rows[i].Cells["uwagi"].Value.ToString();
                            if (x == borA + 10)
                            {
                                x += 11;
                                borA += 37;

                                //POTĘŻNY nagłówek
                                xcel.Range[$"B{x - 5}", $"C{x - 5}"].Merge();
                                xcel.Cells[x - 5, 2] = "Spis dokumentacji niearchiwalnej (aktowej)";

                                xcel.Range[$"B{x - 4}", $"C{x - 4}"].Merge();
                                xcel.Cells[x - 4, 2] = "przeznaczonej na makulaturę lub zniszczenie";

                                xcel.Range[$"B{x - 5}", $"C{x - 4}"].Font.Bold = true;
                                xcel.Range[$"B{x - 5}", $"C{x - 4}"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


                                //podpis jest jako range bo oryginalnie miał być na 2 komórkach ale był za duży i nie chce mi się zmieniać
                                xcel.Range[$"A{x - 6}", $"A{x - 6}"].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                                xcel.Range[$"A{x - 6}", $"A{x - 6}"].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDot;

                                //nagłówki
                                xcel.Cells[x - 1, 1] = "symbol z wykazu akt";
                                xcel.Cells[x - 1, 2] = "tytuł teczki";
                                xcel.Cells[x - 1, 3] = "daty skrajne";
                                xcel.Cells[x - 1, 4] = "liczba tomów";
                                xcel.Cells[x - 1, 5] = "uwagi";

                                xcel.Range[$"A{x - 1}",$"A{x - 1}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                                xcel.Range[$"B{x - 1}",$"B{x - 1}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                                xcel.Range[$"C{x - 1}",$"C{x - 1}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                                xcel.Range[$"D{x - 1}",$"D{x - 1}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                                xcel.Range[$"E{x - 1}",$"E{x - 1}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                                
                            }
                            else
                            {
                                x++;
                            }
                        }

                        //zostawiam to tu na wszelki wypadek
                        //xcel.Cells[x - 1, 1] = "";
                        //xcel.Cells[x - 1, 2] = "";
                        //xcel.Cells[x - 1, 3] = "";
                        //xcel.Cells[x - 1, 4] = "";
                        //xcel.Cells[x - 1, 5] = "";

                        
                        //xcel.Range[$"A{x - 6}", $"B{x - 6}"].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;

                        //xcel.Range[$"B{x - 5}", $"C{x - 5}"].UnMerge();
                        //xcel.Cells[x - 5, 2] = "";

                        //xcel.Range[$"B{x - 4}", $"C{x - 4}"].UnMerge();
                        //xcel.Cells[x - 4, 2] = "";

                        xcel.Columns.AutoFit();
                        xcel.Visible = true;
                        break;


                    case false:
                        for (int i = 2; i < grid.Columns.Count; i++)
                        {
                            xcel.Cells[1, i] = grid.Columns[i - 1].HeaderText;
                        }

                        for (int i = 0; i < grid.Rows.Count - 1; i++)
                        {
                            for (int j = 1; j < grid.Columns.Count; j++)
                            {
                                xcel.Cells[i + 2, j + 1] = grid.Rows[i].Cells[j].Value.ToString();

                            }

                        }
                        for (int i = 1; i < grid.Rows.Count + 1; i++)
                        {
                            xcel.Range[$"B{i}", $"B{i}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                            xcel.Range[$"C{i}", $"C{i}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                            xcel.Range[$"D{i}", $"D{i}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                            xcel.Range[$"E{i}", $"E{i}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                            xcel.Range[$"F{i}", $"F{i}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                            xcel.Range[$"G{i}", $"G{i}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                            xcel.Range[$"H{i}", $"H{i}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                            xcel.Range[$"I{i}", $"I{i}"].BorderAround2(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                        }
                        xcel.Columns.AutoFit();
                        xcel.Visible = true;
                        break;
                    default:
                        MessageBox.Show("uh oh");
                        break;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void KopiujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0) return;
            StringBuilder sb = new StringBuilder();
            int id = grid.SelectedRows[0].Index;
            for (int i = 1; i < grid.ColumnCount; i++)
            {
                sb.Append(grid[i, id].Value+" ");
            }
            Clipboard.SetText(sb.ToString());

        }

        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            KopiujToolStripMenuItem_Click(sender, e);
        }

        private void DelAll_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy na pewno chcesz usunąć wszystkie elementy z tej tabeli?", "Pytanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            string sql = "DELETE FROM archiwum.archiwum WHERE ADDDATE(dat_konc, INTERVAL lat_waz YEAR) < CURRENT_DATE";
            using (MySqlCommand cmd = new MySqlCommand(sql, GlobalData.connection))
            {
                cmd.ExecuteNonQuery();
            }

            backup();


            RefreshGrid("WHERE ADDDATE(dat_konc, INTERVAL lat_waz YEAR) < CURRENT_DATE");
            MessageBox.Show("wszystkie pozycje zostały pomyślnie usunięte");
        }

        private void backup()
        {
            try
            {
                //fajny backup btw
                string date = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace("-", "");
                string path = $@"bin\backups\bu{date}.txt";

                grid.MultiSelect = true;
                grid.SelectAll();
                string txt = grid.GetClipboardContent().GetText();

                File.WriteAllText(path, txt, Encoding.Unicode);
                grid.MultiSelect = false;
            }
            catch (Exception)
            {
                return;
            }
        }


    }
}
