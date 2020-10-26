using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            try
            {
                string path = ConfigurationManager.AppSettings["path"];
                Process.Start(path);
            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            string cs = ConfigurationManager.AppSettings["cs"];
            try
            {
                cs = string.Format(cs, "root", "root");
                GlobalData.connection = new MySqlConnection(cs);
                GlobalData.connection.Open();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            lblStatus.Text = "";
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void RefreshGrid(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT `index` AS id, lp, symbol_wykaz_akt, tytul, dat_pocz, dat_konc, ltomow, uwagi, dodat_info FROM archiwum.archiwum" + " ");
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
                delAll.Visible = false;
                delAllDiv.Visible = false;
            }
            else
            {
                delAll.Visible = true;
                delAllDiv.Visible = true;
            }
        }

        private void Logo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program oraz baza danych zostały stworzona przeze mnie czyli Mateusza Drogowskiego","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
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

            DialogResult result = MessageBox.Show("Czy napewno chcesz usunąć zaznaczone zapis?", "Pytanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;
            string sql = "DELETE FROM archiwum.archiwum WHERE `index` = @id";

            int selecedIndex = grid.SelectedRows[0].Index;
            int id = Convert.ToInt32(grid["id", selecedIndex].Value);

            using (MySqlCommand delete = new MySqlCommand(sql, GlobalData.connection))
            {
                delete.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                delete.ExecuteNonQuery();
            }
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
                        MessageBox.Show("Czujesz to?..Tak to program się skopcił", "Zgon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblStatus.Text = ";)";
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
                        for (int i = 2; i < grid.Columns.Count + 1; i++)
                        {
                            xcel.Cells[1, i] = grid.Columns[i - 1].HeaderText;
                        }

                        for (int i = 0; i < grid.Rows.Count; i++)
                        {
                            for (int j = 0; j < grid.Columns.Count; j++)
                            {
                                xcel.Cells[i + 2, j + 1] = grid.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                        xcel.Columns.AutoFit();
                        xcel.Visible = true;
                        break;


                    case false:
                        for (int i = 1; i < grid.Columns.Count + 1; i++)
                        {
                            xcel.Cells[1, i] = grid.Columns[i - 1].HeaderText;
                        }

                        for (int i = 0; i < grid.Rows.Count; i++)
                        {
                            for (int j = 0; j < grid.Columns.Count; j++)
                            {
                                xcel.Cells[i + 2, j + 1] = grid.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                        xcel.Columns.AutoFit();
                        xcel.Visible = true;
                        break;
                    default:
                        MessageBox.Show("Nwm co się stało ale nie powinno się to stać");
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
            RefreshGrid("WHERE ADDDATE(dat_konc, INTERVAL lat_waz YEAR) < CURRENT_DATE");
        }
    }
}
