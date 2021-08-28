using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using SQLiteAgenda.Tablas;
using System.Collections.ObjectModel;
using System.IO;
using SQLiteAgenda.Datos;

namespace SQLiteAgenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Consulta : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Product> ProductTable; //TablaContacto;
        public V_Consulta()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
           // ListaContactos.ItemSelected += ListaContactos_ItemSelected;
            ProductList.ItemSelected += ProductList_ItemSelected;
            btn_buy.Clicked += Btn_buy_Clicked;
        }

        private void ProductList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Product)e.SelectedItem;
            var item = Obj.Id.ToString();
            var cos = Obj.Cost;
            var nam = Obj.Name;
            var bar = Obj.Barcode;
            int ID = Convert.ToInt32(item);
            try 
            {
                Navigation.PushAsync(new V_Detalle(ID, nam, bar, cos));
            }
            catch(Exception)
            {
                throw;
            }
        }

        protected async override void OnAppearing()
        {
            var ResulRegistros = await conexion.Table<T_Product>().ToListAsync();
            ProductTable = new ObservableCollection<T_Product>(ResulRegistros);
            ProductList.ItemsSource = ProductTable;
            base.OnAppearing();
        }
        private void Btn_buy_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Buy these Products? ", "BUY", "OK");
        }
    }
}