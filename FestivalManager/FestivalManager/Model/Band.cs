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
    class Band
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public Genre Genre { get; set; }
        public LineUp Lineup { get; set; }

        public static ObservableCollection<Band> lstAlleBands = GetBands();

        //methoede die list van alle bands teruggeeft.
        public static ObservableCollection<Band> GetBands()
        {
            ObservableCollection<Band> bandlist = new ObservableCollection<Band>();
            //bandlist.Add(new Band() { ID = "1", Name = "test", Picture = "/url", Description = "/", Twitter = "/", Facebook = "/", Genres = null });
            //bandlist.Add(new Band() { ID = "2", Name = "qfqfqd", Picture = "/url", Description = "qdfsd", Twitter = "/", Facebook = "/", Genres = null });

            String sql = "Select * FROM band";
            //SELECT * FROM Band INNER JOIN genre ON Band.GenreID = Genre.GenreID
            DbDataReader reader = Database.GetData(sql);
            Database.GetData(sql);

            while (reader.Read())
            {
                bandlist.Add(MaakBand(reader));
            }
            return bandlist;
        }


        private static Band MaakBand(IDataRecord rij)
        {
            Band nieuw = new Band();
            Genre nieuwgenre = new Genre();
            nieuw.ID = Convert.ToInt32(rij["BandID"].ToString());
            nieuw.Name = rij["Name"].ToString();
            nieuw.Picture = "/";
            nieuw.Description = rij["Description"].ToString();
            nieuw.Twitter = rij["Twitter"].ToString();
            nieuw.Facebook = rij["Facebook"].ToString();
            Genre Genre = new Genre();
            Genre = Genre.GetGenreByID(Convert.ToInt32(rij["GenreID"]));
            nieuw.Genre = Genre;
            LineUp Lineup = new LineUp();
            Lineup = LineUp.GetLineUpByID(Convert.ToInt32(rij["LineUpID"]));
            nieuw.Lineup = Lineup;

            return nieuw;
        }

        // methode om een band toe te voegen
        public static int AddBand(Band NewBand)
        {
            DbTransaction trans = null;

            int aantalLineup = 0; 
            aantalLineup += LineUp.GetLineupCount();


            try
            {
                trans = Database.BeginTransaction();

                string sql = "INSERT INTO band(Band.Name,Band.Description,Band.Twitter,Band.Facebook,Band.GenreID,Band.LineUpID) VALUES (@name,@description,@twitter,@facebook,@genreid,@LineUpID)";
                DbParameter par1 = Database.AddParameter("@name", NewBand.Name);
                DbParameter par2 = Database.AddParameter("@description", NewBand.Description);
                DbParameter par3 = Database.AddParameter("@twitter", NewBand.Twitter);
                DbParameter par4 = Database.AddParameter("@facebook", NewBand.Facebook);
                DbParameter par5 = Database.AddParameter("@genreid", NewBand.Genre.ID);
                DbParameter par6 = Database.AddParameter("@LineUpID", aantalLineup);
                int rowsaffected = 0;
                rowsaffected += Database.ModifyData(trans, sql, par1, par2, par3, par4, par5, par6);
                if (rowsaffected == 1)
                {
                    MessageBox.Show("Toevoegen is gelukt", "Gelukt", System.Windows.MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Toevoegen mislukt", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
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

        //Name,picture,Description,Twitter,Facebook,GenreID, LineupID
        public static int EditBand(Band band)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();

                string sql = "UPDATE band SET Name=@name, picture=@picture, Description=@Description, Twitter=@Twitter, Facebook=@Facebook, GenreID=@GenreID, LineupID=@LineupID WHERE BandID=@BandID";
                DbParameter par1 = Database.AddParameter("@name", band.Name);
                DbParameter par2 = Database.AddParameter("@picture", null);
                DbParameter par3 = Database.AddParameter("@Description", band.Description);
                DbParameter par4 = Database.AddParameter("@Twitter", band.Twitter);
                DbParameter par5 = Database.AddParameter("@Facebook", band.Facebook);
                DbParameter par6 = Database.AddParameter("@GenreID", band.Genre.ID);
                DbParameter par7 = Database.AddParameter("@LineupID", band.Lineup.ID);
                DbParameter par8 = Database.AddParameter("@BandID", band.ID);
                int rowsaffected = 0;
                rowsaffected += Database.ModifyData(trans, sql, par1, par2, par3, par4, par5, par6, par7, par8);
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

        public static int DeleteBand(Band band)
        {
            DbTransaction trans = null;
            try
            {
                trans = Database.BeginTransaction();
                string sql = "DELETE FROM Band WHERE BandID = @ID;";

                DbParameter parID = Database.AddParameter("@ID", band.ID);
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

        //deze methode zorgt ervoor dat je bands kan opzoeken op naam
        public static ObservableCollection<Band> GetBandsByString(string search)
        {
            ObservableCollection<Band> lstGevondenBands = new ObservableCollection<Band>();
            foreach (Band band in lstAlleBands)
            {
                if (band.Name.ToUpper().Contains(search.ToUpper()) || band.Description.ToUpper().Contains(search.ToUpper()) || band.Facebook.ToUpper().Contains(search.ToUpper()) || band.Twitter.ToUpper().Contains(search.ToUpper()))
                {
                    lstGevondenBands.Add(band);
                }
            }
            return lstGevondenBands;
        }


    }
}
