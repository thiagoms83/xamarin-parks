using Newtonsoft.Json;
using Parks.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parks.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private Account account;
        private AccountStore store;
        private OAuth2Authenticator authenticator;
        public static OAuth2Authenticator Authenticator { get; set; }


       

        public Login()
        {
            InitializeComponent();
            // objetos do tipo Account para um serviço podem ser recuperados
            // chamando o método FindAccountsForService, que retorna uma coleção
            // de objectos Account, com o primeiro item sendo o procurado.
            store = AccountStore.Create();
            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();
           

        }

        override
       protected void OnAppearing()
        {
            var texto = "Bem vindo";
            if (account != null)
            {
                texto += ", " + account.Username;
            }
            usuarioLogado.Text = texto;
        }

        public void BypassLoginCommand(object sender, EventArgs e)
        {
            if (account != null)
            {
                App.IrParaAplicacao();
            }
        }

            public void OnLoginGoogle_Clicked(object sender, EventArgs e)
        {

            string myClientId = null;
            string myRedirectUri = null;
            string myClientSecret = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    myClientId = Constants.iOSGoogleClientId;
                    myRedirectUri = Constants.iOSGoogleRedirectUrl;
                    break;
                case Device.Android:
                    myClientId = Constants.AndroidGoogleClientId;
                    myRedirectUri = Constants.AndroidGoogleRedirectUrl;
                    break;
                case Device.UWP:
                    myClientId = Constants.OutroGoogleClientId;
                    myClientSecret = Constants.OutroGoogleClientSecret;
                    break;
            }
            authenticator = new OAuth2Authenticator(
            clientId: myClientId,
            clientSecret: myClientSecret,
            scope: Constants.GoogleScope,
            authorizeUrl: new Uri(Constants.GoogleAuthorizeUrl),
            redirectUrl: new Uri(myRedirectUri),
            accessTokenUrl: new Uri(Constants.GoogleAccessTokenUrl),
            getUsernameAsync: null,
            isUsingNativeUI: true
            );
            Authenticator = authenticator;
            DoAuthetication();
        }
        public void DoAuthetication()
        {
            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);

        }

        public async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (sender is OAuth2Authenticator authenticator)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }
            if (e.IsAuthenticated)
            {
                User user =  await GetGoogleUserInfo(e.Account);

                if (account != null)
                {
                    store.Delete(account, Constants.AppName);
                }
                e.Account.Username = user.Name;
                await store.SaveAsync(account = e.Account, Constants.AppName);

                App.IrParaAplicacao();
                //await DisplayAlert("Sucesso", "Usuário: " + user.Name, "OK");
            }
        }


        public void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            if (sender is OAuth2Authenticator authenticator)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }
            // mostra mensagem de erro
            DisplayAlert("Authentication error", e.Message, "OK");
        }

        // Obtem dados do usuário
        private async Task<User> GetGoogleUserInfo(Account account)
        {
            var request = new OAuth2Request("GET",
            new Uri(Constants.GoogleUserInfoUrl),
            null, account);
            var response = await request.GetResponseAsync();
            if (response == null)
                return null;
            // "Desserializa" os dados e armazena-os no account store.
            // O email do usuário será usado para identificá-lo no banco de dados
            string userJson = await response.GetResponseTextAsync();
            User user = JsonConvert.DeserializeObject<User>(userJson);
            // email não pode ser vazio
            if (string.IsNullOrEmpty(user.Email))
                user.Email = user.Id + "@Google";
            return user;
        }
    }
}