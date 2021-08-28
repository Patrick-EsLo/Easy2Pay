using SQLiteAgenda.Datos;
using SQLiteAgenda.Menu;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SQLiteAgenda
{
    public partial class App : Application
    {

        static UserDatabaseController userDatabase;
        public App()
        {
            InitializeComponent();

            // MainPage = new NavigationPage(new Vistas.V_Principal());
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static UserDatabaseController UserDatabase
        {
            get
            {
                if (userDatabase == null)
                {
                    userDatabase = new UserDatabaseController();
                }
                return userDatabase;
            }
        }
    }
}
