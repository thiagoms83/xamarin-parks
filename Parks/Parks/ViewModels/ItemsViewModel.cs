using Parks.JSON;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using Xamarin.Forms;
using System.IO;

namespace Parks
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Park> Parks { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            Parks = new ObservableCollection<Park>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Item;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Parks.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                var items = await GetParksAsync();
                foreach (var item in items)
                {
                    Parks.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<IEnumerable<Park>> GetParksAsync()
        {
            var assembly = typeof(AboutPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Parks.Assets.data.data.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            List<Park> parks = JsonConvert.DeserializeObject<List<Park>>(text);
            foreach(Park park in parks) {
                park.Imagem = await getThumb(park.Image); 
            }
            return parks;

        }

        private async Task<ImageSource> getThumb(string filename)
        {
            var assembly = typeof(AboutPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Parks.Assets.img.thumbs." + filename);
            return ImageSource.FromStream(() => stream);
        }
    }

   
}
