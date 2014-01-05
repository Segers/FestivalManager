using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_MVVM.Model
{
    class Festival
    {
        public String Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Location { get; set; }


        public static Festival GetFestivalInfo()
        {
            Festival festivalList = new Festival();
            String sql = "SELECT * FROM festival WHERE FestivalID = 1";
            DbDataReader reader = Database.GetData(sql);
            Database.GetData(sql);

            while (reader.Read())
            {
                festivalList = (MaakFestivalAan(reader));
            }
            return festivalList;
        }

        private static Festival MaakFestivalAan(IDataRecord rij)
        {
            Festival nieuw = new Festival();
            nieuw.Name = rij["Name"].ToString();
            nieuw.StartDate = Convert.ToDateTime(rij["StartDate"].ToString());
            nieuw.EndDate = Convert.ToDateTime(rij["EndDate"].ToString());
            nieuw.Location = rij["Location"].ToString();

            return nieuw;
        }

        public static int EditFestival(Festival festival)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();

                string sql = "UPDATE festival SET Name=@name, StartDate=@startdate, EndDate=@enddate, Location=@location WHERE FestivalID=@festivalID";
                DbParameter par1 = Database.AddParameter("@name", festival.Name);
                DbParameter par2 = Database.AddParameter("@startdate", festival.StartDate.ToString());
                DbParameter par3 = Database.AddParameter("@enddate", festival.EndDate.ToString());
                DbParameter par4 = Database.AddParameter("@location", festival.Location);
                DbParameter par5 = Database.AddParameter("@festivalID", 1);
                int rowsaffected = 0;
                rowsaffected += Database.ModifyData(trans, sql, par1, par2, par3, par4, par5);
                if (rowsaffected == 1)
                {
                    MessageBox.Show("Wijzigen is gelukt", "Gelukt", System.Windows.MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Wijzigen mislukt", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                trans.Commit();
                return rowsaffected;
            }
            catch (Exception)
            {
                trans.Rollback();
                return 0;
            }
        }
    }
}
