using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using SQLiteAgenda.Tablas;
using SQLiteAgenda.Datos;

namespace SQLiteAgenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Registro : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public V_Registro()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnAdd.Clicked += BtnAdd_Clicked;
        }
        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            var Productdata = new T_Product
            {
                Name = txtName.Text,
                Barcode = txtBarcode.Text,
                Cost = txtCost.Text
            };
            conexion.InsertAsync(Productdata);
            CleanFormulario();
            DisplayAlert("Confirmation", "The Product was register correct", "OK");
        }
        private void CleanFormulario()
        {
            txtName.Text = "";
            txtBarcode.Text = "";
            txtCost.Text = "";
        }
    }
}