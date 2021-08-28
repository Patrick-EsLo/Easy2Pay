using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteAgenda.Models;
using SQLiteAgenda.Menu;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLiteAgenda.Vistas;

namespace SQLiteAgenda.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {

            BackgroundColor = Constants.BackgroundColor;
            Lbl_Username.TextColor = Constants.MainTextColor;
            Lbl_Password.TextColor = Constants.MainTextColor;
            LoginIcon1.HeightRequest = Constants.LogInIconHeight;

      

            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
            Entry_Password.Completed += (s, e) => SignInProcedure(s, e);

        }
        async void SignInProcedure(object sender, EventArgs e)
        {
            User user = new User(Entry_Username.Text, Entry_Password.Text);
            if (user.CheckInformatLgn())
            {

                // var result = await App.RestService.Login(user);//Para testar é só comentar essa linha

             //   var result = new Token();//Para testar é só descomentar essa linha
                await DisplayAlert("Login", "Login Success", "ok");

     

                //if (result.access_token != null)//Para testar é só comentar essa linha

               
                    //App.UserDatabase.SaveUser(user);//Para testar é só comentar essa linha
                    // App.TokenDatabase.SaveToken(result);//Para testar é só comentar essa linha

                    if (Device.OS == TargetPlatform.Android)
                    {
                        Application.Current.MainPage = new NavigationPage(new V_Principal());
                    }
                    else if (Device.OS == TargetPlatform.iOS)
                    {
                        await Navigation.PushModalAsync(new NavigationPage(new V_Principal()));
                    }
                    else
                    {
                        await Navigation.PushAsync(new V_Principal()); // might not work for UWP
                    }
                

            }
            else
            {
                await DisplayAlert("Login", "Login Not Correct", "ok");

            }
        }
    }
}