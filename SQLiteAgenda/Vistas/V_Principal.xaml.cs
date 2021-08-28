using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ZXing;
using ZXing.Net.Mobile.Forms;

using SQLite;
using SQLiteAgenda.Tablas;
using System.IO;
using SQLiteAgenda.Datos;

namespace SQLiteAgenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Principal : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public V_Principal()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnSearch.Clicked += BtnSearch_Clicked;
            btnRegister.Clicked += BtnRegister_Clicked;
            btnScan.Clicked += BtnScan_Clicked;
        }


        private void BtnRegister_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_Registro());
        }
        private void BtnScan_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScannerPage());
        }


        private void BtnSearch_Clicked(object sender, EventArgs e)
        {
            try
            {
                var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AgendaSQLite.db3");
                var db = new SQLiteConnection(rutaDB);
                db.CreateTable<T_Product>();
                IEnumerable<T_Product> resultado = SELECT_WHERE(db, txtName.Text);//txtName e mudar no xaml txtName para txtBarcode
                if(resultado.Count()>0)
                {
                    Navigation.PushAsync(new V_Consulta());
                    DisplayAlert("Warning", "There is a product with that Name", "OK");
                }
                else 
                {
                    DisplayAlert("Warning", "There isn't a product with that Name", "OK");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    

    private IEnumerable<T_Product> SELECT_WHERE(SQLiteConnection db, string name)//name
        {
            return db.Query<T_Product>("SELECT * FROM T_Product WHERE Name=?", name);//name
        }
    }
}