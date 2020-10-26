using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiwum
{
    class GlobalData
    {
        public static MySqlConnection connection;
        public static String Stat;
        public static Int32 lp;
        public static String sWa;
        public static String tyt;
        public static String dat_pocz;
        public static String dat_konc;
        public static Int32 ltomow;
        public static String uwagi;
        public static String dodat_info;
        public static Int32 ID;
    }
}
