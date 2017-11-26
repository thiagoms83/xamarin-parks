using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using Parks.Views;

namespace Parks.Droid.Implementations
{
    [Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(
new[] { Intent.ActionView },
Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
DataSchemes = new[] { "com.googleusercontent.apps.833822264754-srqnb0715inv6lvk65j2uru8p99lfjd7" },
DataPath = "/oauth2redirect")]
    public class CustomUrlSchemeInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Converte Android.Net.Url para Uri
            var uri = new Uri(Intent.Data.ToString());
            // Carrega página de redirecionamento
            Login.Authenticator.OnPageLoading(uri);
            // encerra essa Activity
            Finish();
        }
    }
}