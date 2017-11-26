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
using Parks.Interfaces;
using Parks.Droid.Implementations;


using Xamarin.Forms;
using Java.IO;
using System.IO;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceSpecific_Droid))]
namespace Parks.Droid.Implementations
{
    public class DeviceSpecific_Droid : IDeviceSpecific
    {
        public async Task<string> LerTextoArquivo(string filename)
        {
   
            Context context = Android.App.Application.Context;
            int id = context.Resources.GetIdentifier(filename, "raw", context.PackageName);
            Stream stream = context.Resources.OpenRawResource(id);
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string value = reader.ReadToEnd();
                return value;
            }
         }
    }
}