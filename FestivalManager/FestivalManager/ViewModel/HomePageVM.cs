using GalaSoft.MvvmLight.Command;
using Oefening1.ViewModel;
using Project_MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace _FestivalManager.ViewModel
{
    class HomePageVM : ObservableObject, IPage
    {



        #region constructor
        public HomePageVM()
        {
            GefilterdeBands = Band.GetBands();
            GefilterdeContacts = Contactperson.GetContacts();
            ContactTypeList = ContactpersonType.GetContactTypes();
            GenreList = Genre.GetGenres();
            SelectedTicketType = new TicketType();
            TicketTypeList = TicketType.GetTicketType(SelectedTicketType);
            NewPodium = new Podium();
            NewBand = new Band();
            NewGenre = new Genre();
            NewTicket = new Ticket();
            NewLineUp = new LineUp();
            NewContactType = new ContactpersonType();
            NewContact = new Contactperson();
            TicketList = Ticket.GetTickets();
            PodiumList = Podium.GetPodia();
            FestivalList = Festival.GetFestivalInfo();
            ContactList = Contactperson.GetContacts();
            
        }

        public string Name
        {
            get { return "Home"; }
        }

        #endregion

        #region Properties
        private ObservableCollection<Band> _bandList;

        public ObservableCollection<Band> BandList
        {
            get
            {
                return _bandList;
            }
            set
            {
                _bandList = value; OnPropertyChanged("BandList");
            }
        }

        private ObservableCollection<Contactperson> _contactList;

        public ObservableCollection<Contactperson> ContactList
        {
            get
            {
                return _contactList;
            }
            set
            {
                _contactList = value; OnPropertyChanged("ContactList");
            }
        }

        private ObservableCollection<ContactpersonType> _contactTypeList;

        public ObservableCollection<ContactpersonType> ContactTypeList
        {
            get
            {
                return _contactTypeList;
            }
            set
            {
                _contactTypeList = value; OnPropertyChanged("ContactTypeList");
            }
        }


        private ObservableCollection<TicketType> _ticketTypeList;

        public ObservableCollection<TicketType> TicketTypeList
        {
            get
            {
                return _ticketTypeList;
            }
            set
            {
                _ticketTypeList = value; OnPropertyChanged("TicketTypeList"); OnPropertyChanged("TicketList");
            }
        }

        private ObservableCollection<Ticket> _ticketList;

        public ObservableCollection<Ticket> TicketList
        {
            get
            {
                return _ticketList;
            }
            set
            {
                _ticketList = value; OnPropertyChanged("TicketList");
            }
        }

        private ObservableCollection<Podium> _podiumList;

        public ObservableCollection<Podium> PodiumList
        {
            get
            {
                return _podiumList;
            }
            set
            {
                _podiumList = value; OnPropertyChanged("PodiumList");
            }
        }

        private ObservableCollection<Genre> _genreList;

        public ObservableCollection<Genre> GenreList
        {
            get
            {
                return _genreList;
            }
            set
            {
                _genreList = value; OnPropertyChanged("GenreList");
            }
        }

        private Festival _festivalList;

        public Festival FestivalList
        {
            get
            {
                return _festivalList;
            }
            set
            {
                _festivalList = value; OnPropertyChanged("FestivalList");
            }
        }

        private TicketType _selectedtickettype;

        public TicketType SelectedTicketType
        {
            get { return _selectedtickettype; }
            set { _selectedtickettype = value; OnPropertyChanged("SelectedTicketType"); }
        }

        private Podium _newPodium;

        public Podium NewPodium
        {
            get { return _newPodium; }
            set { _newPodium = value; OnPropertyChanged("NewPodium"); }
        }


        private Genre _newGenre;
        public Genre NewGenre
        {
            get { return _newGenre; }
            set { _newGenre = value; OnPropertyChanged("NewGenre"); }
        }

        private Band _newBand;
        public Band NewBand
        {
            get { return _newBand; }
            set { _newBand = value; OnPropertyChanged("NewBand"); }
        }

        private Ticket _newTicket;
        public Ticket NewTicket
        {
            get { return _newTicket; }
            set { _newTicket = value; OnPropertyChanged("NewTicket"); }
        }

        private ContactpersonType _newContactType;
        public ContactpersonType NewContactType
        {
            get { return _newContactType; }
            set { _newContactType = value; OnPropertyChanged("NewContactType"); }
        }

        private Contactperson _newContact;
        public Contactperson NewContact
        {
            get { return _newContact; }
            set { _newContact= value; OnPropertyChanged("NewContact"); }
        }

        private LineUp _newLineUp;
        public LineUp NewLineUp
        {
            get { return _newLineUp; }
            set { _newLineUp = value; OnPropertyChanged("NewLineUp"); }
        }

        private ObservableCollection<Contactperson> _gefilterdeContacts;
        public ObservableCollection<Contactperson> GefilterdeContacts
        {
            get { return _gefilterdeContacts; }
            set { _gefilterdeContacts = value; OnPropertyChanged("GefilterdeContacts"); }
        }

        private ObservableCollection<Band> _gefilterdeBands;
        public ObservableCollection<Band> GefilterdeBands
        {
            get { return _gefilterdeBands; }
            set { _gefilterdeBands = value; OnPropertyChanged("GefilterdeBands"); }
        }

        private string _editvis;
        public string EditVis
        {
            get { return _editvis; }
            set { _editvis = value; OnPropertyChanged("EditVis"); }
        }
        private string _newvis;

        public string NewVis
        {
            get { return _newvis; }
            set { _newvis = value; OnPropertyChanged("NewVis"); }
        }
        
        #endregion


        #region Commands Band
        public ICommand addBandCommand
        {
            get { return new RelayCommand(AddBand); }
        }

        private void AddBand()
        {
            if (NewBand.Name != null && NewBand.Description != null && NewBand.Facebook != null && NewBand.Genre != null && NewBand.Twitter != null)
            {
                int i2 = LineUp.AddLineUp(NewLineUp);


                if (i2 != 0)
                {
                    int i = Band.AddBand(NewBand);
                    if (i != 0)
                    {
                        GefilterdeBands = Band.GetBands();
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Er mogen geen lege velden zijn!", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }



        public ICommand EditBandCommand
        {
            get { return new RelayCommand(EditBand); }
        }
        public void EditBand()
        {
            if (NewBand.Name != null && NewBand.Genre != null)
            {

                int i2 = LineUp.EditLineUp(NewLineUp, NewBand);
                
                if (i2 != 0)
                {
                    int i = Band.EditBand(NewBand);
                    if (i != 0)
                    {
                        GefilterdeBands = Band.GetBands();
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Er mogen geen lege velden zijn!", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public ICommand DeleteBandCommand
        {
            get { return new RelayCommand(DeleteBand); }
        }
        public void DeleteBand()
        {
            if (NewBand.Name != null)
            {

                
                int i = Band.DeleteBand(NewBand);
                int i2 = LineUp.DeleteLineup(NewBand);
                if (i != 0 && i2 != 0)
                {
                    GefilterdeBands = Band.GetBands();
                    
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Gelieve een band te selecteren", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
        }

        public ICommand SearchBandCommand
        {
            get { return new RelayCommand<string>(SearchBand); }
        }
        private void SearchBand(string str)
        {
            Console.WriteLine(str);
            GefilterdeBands = Band.GetBandsByString(str);
            BandList = Band.GetBands();
        }
        #endregion

        #region Commands TicketType

        public ICommand addTicketTypeCommand
        {
            get { return new RelayCommand(AddTicketType); }
        }

        private void AddTicketType()
        {
            if (SelectedTicketType.Name != null && SelectedTicketType.Price != 0.0 && SelectedTicketType.AvailableTickets != 0)
            {

                int i = TicketType.AddType(SelectedTicketType);
                if (i != 0)
                {
                    TicketTypeList = TicketType.GetTicketType(SelectedTicketType);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Er mogen geen lege velden zijn!", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }


        public ICommand EditicketTypeCommand
        {
            get { return new RelayCommand(EditicketType); }
        }
        public void EditicketType()
        {
            if (SelectedTicketType.Name != null)
            {

                int i = TicketType.EditType(SelectedTicketType);
                if (i != 0)
                {
                    TicketTypeList = TicketType.GetTicketType();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Er mogen geen lege velden zijn!", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        #endregion

        #region Commands Podium

        public ICommand SaveStageCommand
        {
            get { return new RelayCommand(SaveStage); }
        }
        public void SaveStage()
        {
            if (NewPodium.Name != null)
            {

                int i = Podium.AddStage(NewPodium);
                if (i != 0)
                {
                    PodiumList = Podium.GetPodia();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Er mogen geen lege velden zijn!", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public ICommand EditStageCommand
        {
            get { return new RelayCommand(EditStage); }
        }
        public void EditStage()
        {
            if (NewPodium.Name != null)
            {

                int i = Podium.EditStage(NewPodium);
                if (i != 0)
                {
                    PodiumList = Podium.GetPodia();
                }
                else
                {
                    System.Windows.MessageBox.Show("Er mogen geen lege velden zijn!", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }
        }


        #endregion

        #region Commands Genre
        public ICommand AddGenreCommand
        {
            get { return new RelayCommand(AddGenre); }
        }
        public void AddGenre()
        {
            if (NewGenre.Name != null)
            {

                int i = Genre.AddGenre(NewGenre);
                if (i != 0)
                {
                    GenreList = Genre.GetGenres();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Er mogen geen lege velden zijn!", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public ICommand EditGenreCommand
        {
            get { return new RelayCommand(EditGenre); }
        }
        public void EditGenre()
        {
            if (NewGenre.Name != null)
            {

                int i = Genre.EditGenre(NewGenre);
                if (i != 0)
                {
                    GenreList = Genre.GetGenres();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Er mogen geen lege velden zijn!", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }



        #endregion

        #region Commands Ticket

        public ICommand SaveTicketCommand
        {
            get { return new RelayCommand(SaveTicket); }
        }
        public void SaveTicket()
        {
            if (NewTicket.Ticketholder != null && NewTicket.TicketholderEmail != null && NewTicket.TicketType.ID != 0 && NewTicket.Amount != 0 )
            {

                int i = Ticket.AddTicket(NewTicket);
                if (i != 0)
                {
                    TicketList = Ticket.GetTickets();
                    TicketTypeList = TicketType.GetTicketType(SelectedTicketType);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Er mogen geen lege velden zijn!", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public ICommand EditTicketCommand
        {
            get { return new RelayCommand(EditTicket); }
        }
        public void EditTicket()
        {
            if (NewTicket.Ticketholder != null)
            {

                int i = Ticket.EditTicket(NewTicket);
                if (i != 0)
                {
                    TicketList = Ticket.GetTickets();
                    TicketTypeList = TicketType.GetTicketType(SelectedTicketType);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Er mogen geen lege velden zijn!", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public ICommand PrintTicketCommand
        {
            get { return new RelayCommand<int>(PrintTicket); }
        }

        private void PrintTicket(int id)
        {
            Ticket ticket = NewTicket;

            if (NewTicket != null)
            {
                String sPad = "";
                Ticket.PrintWord(ticket, sPad);
            }


        }

        #endregion


        #region Commands ContactType

        public ICommand AddContactTypeCommand
        {
            get { return new RelayCommand(AddContactType); }
        }
        public void AddContactType()
        {
            if (NewContactType.Name != null)
            {

                int i = ContactpersonType.AddContactType(NewContactType);
                if (i != 0)
                {
                    ContactTypeList = ContactpersonType.GetContactTypes();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Er mogen geen lege velden zijn!", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public ICommand EditConactTypeCommand
        {
            get { return new RelayCommand(EditContactType); }
        }
        public void EditContactType()
        {
            if (NewContactType.Name != null)
            {

                int i = ContactpersonType.EditContactType(NewContactType);
                if (i != 0)
                {
                    ContactTypeList = ContactpersonType.GetContactTypes();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Er mogen geen lege velden zijn!", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        #endregion

        #region Commands Contact

        public ICommand AddContactCommand
        {
            get { return new RelayCommand(AddContact); }
        }
        public void AddContact()
        {
            if (NewContact.Name != null)
            {

                int i = Contactperson.AddContact(NewContact);
                if (i != 0)
                {
                    GefilterdeContacts = Contactperson.GetContacts();
                    
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Er mogen geen lege velden zijn!", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public ICommand EditConactCommand
        {
            get { return new RelayCommand(EditContact); }
        }
        public void EditContact()
        {
            if (NewContact.Name != null)
            {

                int i = Contactperson.EditContact(NewContact);
                if (i != 0)
                {
                    GefilterdeContacts = Contactperson.GetContacts();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Er mogen geen lege velden zijn!", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }


        public ICommand DeleteConactCommand
        {
            get { return new RelayCommand(DeleteConact); }
        }
        public void DeleteConact()
        {
            if (NewContact.Name != null)
            {

                int i = Contactperson.DeleteContact(NewContact);
                if (i != 0)
                {
                    GefilterdeContacts = Contactperson.GetContacts();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Gelieve een contact te selecteren", "Mislukt", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
        }

        public ICommand SearchContactCommand
        {
            //Een gebruiker heeft de mogelijkheid om adhv een zoekbox te zoeken naar contactpersonen.
            //Bij ieder karakter dat ingedrukt wordt zal er gezocht op alle informatie worden en de gevonden contactpersonen terug geven
            get { return new RelayCommand<string>(Search); }
        }
        private void Search(string str)
        {
            Console.WriteLine(str);
            GefilterdeContacts = Contactperson.GetContactsByString(ContactList, str);
            ContactList = Contactperson.GetContacts();
        }


        #endregion

        #region Commands Festival

        public ICommand EdiFestivalCommand
        {
            get { return new RelayCommand(EditFestival); }
        }
        public void EditFestival()
        {
            if (FestivalList.Name != null)
            {

                int i = Festival.EditFestival(FestivalList);
                if (i != 0)
                {
                    FestivalList = Festival.GetFestivalInfo();
                }
            }
        }

        #endregion
        //#region Interface
        //public string Name
        //{
        //    get { throw new NotImplementedException(); }
        //}
        //#endregion


    }
}

    


