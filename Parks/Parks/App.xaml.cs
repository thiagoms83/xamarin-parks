using Parks.Views;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Parks
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //if (Device.RuntimePlatform == Device.iOS)
            //    MainPage = new MainPage();
            //else
            // MainPage = new NavigationPage(new MainPage());
            IrParaLogin();
        }


        public static async void IrParaLogin()
        {
            if (Device.RuntimePlatform == Device.iOS)
                App.Current.MainPage = new Login();
            else
                App.Current.MainPage = new NavigationPage(new Login());
        }

        public static async void IrParaAplicacao()
        {
            if (Device.RuntimePlatform == Device.iOS)
                App.Current.MainPage = new MainPage();
            else
                App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}