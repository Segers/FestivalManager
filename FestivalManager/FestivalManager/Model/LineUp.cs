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
    class LineUp
    {
        public int ID { get; set; }
        public string From { get; set; }
        public string Until { get; set; }
        public Podium Podium { get; set; }

        //Zoekt het hoogst ID op, zodat in Band.Addband een nieuw lineupID aangemaakt kan worden.
        public static int GetLineupCount()
        {
            int aantallineups = 0;
            String sql = "SELECT Max(LineUpID) as aantal FROM Lineup";
            DbDataReader reader = Database.GetData(sql);
            Database.GetData(sql);

            while (reader.Read())
            {
                aantallineups += MaakAantalAan(reader);

            }
            return aantallineups;
        }

        //zoekt een lineup op basis van ID
        public static LineUp GetLineUpByID(int id)
        {
            LineUp gevondenLineUp = new LineUp();
            DbParameter par = Database.AddParameter("@ID", id);
            DbDataReader reader = Database.GetData("SELECT * FROM LineUp WHERE LineUpID=@ID", par);
            while (reader.Read())
            {
                gevondenLineUp = MaakLineupAan(reader);
                return gevondenLineUp;
            }

            return gevondenLineUp;
        }

        public static LineUp MaakLineupAan(IDataRecord rij)
        {
            LineUp nieuw = new LineUp();
            nieuw.ID = Convert.ToInt32(rij["LineUpID"].ToString());
            nieuw.From = rij["From"].ToString();
            nieuw.Until = rij["Until"].ToString();
            Podium Podium = new Podium();
            Podium =Podium.GetStageByID(Convert.ToInt32(rij["StageID"]));
            nieuw.Podium = Podium;

            return nieuw;
        }

        public static int MaakAantalAan(IDataRecord rij)
        {
            try
            {
                int aantal;
                aantal = Convert.ToInt32(rij["aantal"].ToString());
                return aantal;
            }
            catch (Exception)
            {
                int aantal = 1;
                return aantal;
            }

            
        }

        public static int AddLineUp(LineUp NewLineUp)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();

                string sql = "INSERT INTO lineup (`From`, `Until`, `StageID`) VALUES (@From, @Until, @StageID);";
                DbParameter par1 = Database.AddParameter("@From", NewLineUp.From);
                DbParameter par2 = Database.AddParameter("@Until", NewLineUp.Until);
                DbParameter par3 = Database.AddParameter("@StageID", NewLineUp.Podium.ID);
                int rowsaffected = 0;
                rowsaffected += Database.ModifyData(trans, sql, par1, par2, par3);
                if (rowsaffected == 1)
                {
                    
                }
                else
                {
                    
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

        public static int EditLineUp(LineUp lineup, Band band)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();

                string sql = "UPDATE `dbfestival`.`lineup` SET `From`=@From, `Until`=@Until WHERE `LineUpID`=@LineupID;";
                DbParameter par1 = Database.AddParameter("@From", lineup.From);
                DbParameter par2 = Database.AddParameter("@Until", lineup.Until);
                DbParameter par3 = Database.AddParameter("@StageID", lineup.Podium.ID);
                DbParameter par4 = Database.AddParameter("@LineupID", band.Lineup.ID);
                int rowsaffected = 0;
                rowsaffected += Database.ModifyData(trans, sql, par1, par2, par3, par4);
                if (rowsaffected == 1)
                {
                    
                }
                else
                {
                    
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

        public static int DeleteLineup(Band band)
        {
            DbTransaction trans = null;
            try
            {
                trans = Database.BeginTransaction();
                string sql = "DELETE FROM lineup WHERE LineUpID = @ID;";

                DbParameter parID = Database.AddParameter("@ID", band.Lineup.ID);
                int rowsaffected = 0;
                rowsaffected += Database.ModifyData(sql, parID);
                if (rowsaffected == 0)
                {
                    MessageBox.Show("Verwijderen mislukt", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error, System.Windows.MessageBoxResult.OK);
                }
                else
                {
                    MessageBox.Show("Succesvol verwijderd", "Gelukt", System.Windows.MessageBoxButton.OK);
                    Console.WriteLine(rowsaffected + " row(s) are deleted");
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
