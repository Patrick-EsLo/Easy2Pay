using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLiteAgenda.Tablas;
using SQLiteAgenda.Datos;
using SQLite;
using System.IO;

namespace SQLiteAgenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Detalle : ContentPage
    {
        public int IdSeleccionado;
        public string NamSelect, BarSelect, CosSelect;//NomSeleccionado, ApSeleccionado, TelSeleccionado;
        private SQLiteAsyncConnection conexion;
        IEnumerable<T_Product> ResultadoDelete;
        IEnumerable<T_Product> ResultadoUpdate;

        public V_Detalle(int id, string nam, string bar, string cos)
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            IdSeleccionado = id;
            NamSelect = nam;
            BarSelect = bar;
            CosSelect = cos;
            btn_update.Clicked += Btn_update_Clicked;
            btn_delete.Clicked += Btn_delete_Clicked;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            lblMensaje.Text = " ID :" + IdSeleccionado;
            txtName.Text = NamSelect;
            txtBarcode.Text = BarSelect;
            txtCost.Text = CosSelect;
        }
        private void Btn_delete_Clicked(object sender, EventArgs e)
        {
            var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AgendaSQLite.db3");
            var db = new SQLiteConnection(rutaDB);
            ResultadoDelete = Delete(db, IdSeleccionado);
            DisplayAlert("Confirmation", "The select product was eliminated", "OK");
            Clean();
        }
        private void Btn_update_Clicked(object sender, EventArgs e)
        {
            var rutadb = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AgendaSQLite.db3");
            var db = new SQLiteConnection(rutadb);
            ResultadoUpdate = Update(db, txtName.Text, txtBarcode.Text, txtCost.Text, IdSeleccionado);
            DisplayAlert("Confirmation", "Product information was update", "OK");
        }
        public static IEnumerable<T_Product> Delete(SQLiteConnection db, int id)
        {
            return db.Query<T_Product>("Delete FROM T_PRODUCT WHERE Id = ?", id);
        }
        public static IEnumerable<T_Product> Update(SQLiteConnection db, string name, string barcode, string cost, int id)
        {
            return db.Query<T_Product>("UPDATE T_Product SET Name = ?, Barcode = ?, Cost = ? WHERE Id =?", name, barcode, cost, id);
        }
        public void Clean()
        {
            lblMensaje.Text = "";
            txtName.Text = "";
            txtBarcode.Text = "";
            txtCost.Text = "";
        }
    }
}