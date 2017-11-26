using Parks.JSON;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using Xamarin.Forms;

namespace Parks
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Park Item { get; set; }
        public ItemDetailViewModel(Park item = null)
        {
            Title = item?.Name;
            Item = item;
            definirImagem();

        }

        private async void definirImagem()
        {
            Imagem = await getHeader(Item.Image);
        }


        private async Task<ImageSource> getHeader(string filename)
        {
            var assembly = typeof(AboutPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Parks.Assets.img.headers." + filename);
            return ImageSource.FromStream(() => stream);
        }
    }
}
