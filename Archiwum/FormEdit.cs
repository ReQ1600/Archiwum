using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archiwum
{
    public partial class FormEdit : Form
    {
        public FormEdit()
        {
            InitializeComponent();


            
        }


        private void BtnAccept_Click(object sender, EventArgs e)
        {
            if (tbTitle.Text.Trim().Equals("")) { MessageBox.Show("Wymagane pola są puste", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; };

            string sql;
            bool exceCatch = false;
            try
            {
                sql = @"Update archiwum.archiwum SET
                lp = @lp , symbol_wykaz_akt = @sWa , tytul = @tyt , dat_pocz = @dat_pocz , dat_konc = @dat_konc , lat_waz = @lat_waz , ltomow = @ltomow , uwagi = @uwagi , dodat_info = @dodat, dskrajne = @dskrajne WHERE `index`= @id";

                if (tbSDate.Text.Replace(" ", "").Equals("--"))
                {
                    sql = @"Update archiwum.archiwum SET
                    lp = @lp , symbol_wykaz_akt = @sWa , tytul = @tyt , dat_pocz = NULL , dat_konc = @dat_konc, lat_waz = @lat_waz , ltomow = @ltomow , uwagi = @uwagi , dodat_info = @dodat, dskrajne = @dskrajne WHERE `index` = @id";
                }
                if (tbEDate.Text.Replace(" ", "").Equals("--"))
                {
                    sql = @"Update archiwum.archiwum SET
                    lp = @lp , symbol_wykaz_akt = @sWa , tytul = @tyt , dat_pocz = NULL , dat_konc = NULL, lat_waz = NULL , ltomow = @ltomow , uwagi = @uwagi , dodat_info = @dodat, dskrajne = NULL WHERE `index` = @id";
                }
                if (tbSDate.Text.Replace(" ", "").Equals("--") && tbEDate.Text.Replace(" ", "").Equals("--"))
                {
                    sql = @"Update archiwum.archiwum SET
                    lp = @lp , symbol_wykaz_akt = @sWa , tytul = @tyt , dat_pocz = NULL , dat_konc = NULL,
                    lat_waz = @lat_waz , ltomow = @ltomow , uwagi = @uwagi , dodat_info, dskrajne = NULL = @dodat WHERE `index` = @id";
                }

                using (MySqlCommand cmd = new MySqlCommand(sql, GlobalData.connection))
                {
                    cmd.Parameters.Add("@lp", MySqlDbType.Int32).Value = num.Value;
                    cmd.Parameters.Add("@sWa", MySqlDbType.VarChar, 20).Value = tbS.Text;
                    cmd.Parameters.Add("@tyt", MySqlDbType.VarChar, 200).Value = tbTitle.Text;
                    cmd.Parameters.Add("@dat_pocz", MySqlDbType.Date).Value = tbSDate.Text;
                    cmd.Parameters.Add("@dat_konc", MySqlDbType.Date).Value = tbEDate.Text;
                    cmd.Parameters.Add("@ltomow", MySqlDbType.Int32).Value = numlT.Value;
                    cmd.Parameters.Add("@dodat", MySqlDbType.VarChar, 50).Value = tbD.Text;

                    if (!(tbSDate.Text.Replace(" ", "").Equals("--") && tbEDate.Text.Replace(" ", "").Equals("--")))
                    {
                        if (tbSDate.Text.Replace(" ", "").Equals("--"))
                        {
                            cmd.Parameters.Add("@dskrajne", MySqlDbType.VarChar, 50).Value = tbEDate.Text;
                        }
                        else if (tbEDate.Text.Replace(" ", "").Equals("--"))
                        {
                            cmd.Parameters.Add("@dskrajne", MySqlDbType.VarChar, 50).Value = tbSDate.Text;
                        }
                        else
                        {
                            cmd.Parameters.Add("@dskrajne", MySqlDbType.VarChar, 50).Value = tbSDate.Text + " - " + tbEDate.Text;

                        }
                    }

                    if (tbU.Text.Replace(" ","").Equals("B-"))
                    {
                        cmd.Parameters.Add("@lat_waz", MySqlDbType.Int32).Value = 0;
                        cmd.Parameters.Add("@uwagi", MySqlDbType.VarChar, 50).Value = null;
                    }
                    else
                    {
                        cmd.Parameters.Add("@lat_waz", MySqlDbType.Int32).Value = Convert.ToInt32(tbU.Text.Remove(0, 2).Replace(" ", ""));
                        cmd.Parameters.Add("@uwagi", MySqlDbType.VarChar, 50).Value = tbU.Text;
                    }
                    cmd.Parameters.Add("@id",MySqlDbType.Int32).Value = GlobalData.ID;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Niewłaściwa wartość w którymś z pól." + "\r(" + exc.Message + ")", "Błąd dodawania", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GlobalData.Stat = "Dodawanie nie powiodło się/zostało anulowane";
                exceCatch = true;
            }
            if (exceCatch == false)
            {
                GlobalData.Stat = "Dodawanie przebiegło pomyślnie";
                DialogResult = DialogResult.OK;
                Close();
            }

            GlobalData.Stat = "Edycja przebiegła pomyślnie";
            DialogResult = DialogResult.OK;
            Close();
        
    }

        private void FormEdit_Load(object sender, EventArgs e)
        {
            num.Value = GlobalData.lp;
            tbS.Text = GlobalData.sWa;
            tbTitle.Text = GlobalData.tyt;
            tbSDate.Text = GlobalData.dat_pocz;
            tbEDate.Text = GlobalData.dat_konc;
            numlT.Value = GlobalData.ltomow;
            tbU.Text = GlobalData.uwagi;
            tbD.Text = GlobalData.dodat_info;
        }

        private void TbEDate_TextChanged(object sender, EventArgs e)
        {
            int x = 0;
            char[] a = tbEDate.Text.Replace(" ", "").ToCharArray();
            for (int i = 0; i <= a.Length; i++)
            {
                x++;
            }
            if (x == 11)
            {
                tbU.Enabled = true;
            }
            else
            {
                tbU.Enabled = false;
            }
        }
    }
}
