using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;
using Xamarin.Forms;
using SQLite;
using SQLiteAgenda.Tablas;
using SQLiteAgenda.Datos;
using System.IO;


namespace SQLiteAgenda.Vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScannerPage : ContentPage
	{
		private SQLiteAsyncConnection conexion;
		public ScannerPage()
		{
			InitializeComponent();
			conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
			//btnScan.Clicked += BtnScan_Clicked;
		}
		private IEnumerable<T_Product> SELECT_WHERE(SQLiteConnection db, string barcode)//name
		{
			return db.Query<T_Product>("SELECT * FROM T_Product WHERE Barcode=?", barcode);//name
		}
		public void Handle_OnScanResult(Result result)
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
				var code = await DisplayAlert("Scanned result", result.Text, "OK", "cancel");
				//var code = await DisplayActionSheet("Add to Product List", "Yes", "No");
				//var ans = await DisplayAlert("Question?", "Would you like Delete", "Yes", "No");
				//Debug.WriteLine("Action: " + code);
				//Debug.WriteLine("Action: " + ans);


				if (code == true) 
				{
					try
					{
						var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AgendaSQLite.db3");
						var db = new SQLiteConnection(rutaDB);
						db.CreateTable<T_Product>();
						IEnumerable<T_Product> resultado = SELECT_WHERE(db, result.Text);//txtName e mudar no xaml txtName para txtBarcode
						if (resultado.Count() > 0)
						{
							await Navigation.PushAsync(new V_Consulta());
							await DisplayAlert("Warning", "There is a product with that Barcode", "OK");
						}
						else
						{
							await DisplayAlert ("Warning", "There isn't a product with that Barcode", "OK");
						}
					}
					catch (Exception)
					{
						throw;
					}
				}
				else
				{
					//false conditon
				}


				
			}
			);

		
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			_scanView.IsScanning = true;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			_scanView.IsScanning = false;
		}
	}
}