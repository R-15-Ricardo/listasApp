using System;
using System.IO;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using static listas.Readers;

using listas.Interface;

namespace listas
{
    class Readers
    {
        public static List<ListItem> readTasksJson()
        {
            string text = DependencyService.Get<IFileService>().ReadFile("tasks.json");
            List<ListItem> tasks = JsonConvert.DeserializeObject<List<ListItem>>(text);

            return tasks;
        }
        public static void writeTasksJson(List<ListItem> tasks)
        {
            string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            DependencyService.Get<IFileService>().WriteFile("tasks.json", json);
        }
    }

    public partial class App : Application
    {
        public App()
        {
            DependencyService.Get<IFileService>().InitializeFile("tasks.json");

            List<ListItem> dates = readTasksJson();

            InitializeComponent();
            MainPage = new NavigationPage(new MainPage()) {BarBackgroundColor = Color.FromHex("#ff4d4d"), HeightRequest = 10};
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
