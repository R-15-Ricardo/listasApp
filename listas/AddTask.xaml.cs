using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static listas.Readers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace listas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTask : ContentPage
    {
        public AddTask()
        {
            InitializeComponent();
        }

        private void saveTaskbtn_Clicked(object sender, EventArgs e)
        {

            List<ListItem> dates = readTasksJson();
            if (dates == null) dates = new List<ListItem>();

            ListItem newTask = new ListItem {
                title = (taskTitle.Text == null) ? $"Tarea#{dates.Count + 1}" : taskTitle.Text,
                description = (taskDescription.Text == null) ? "Esta es una tarea!" : taskDescription.Text,
                date = (taskDate.Date < DateTime.Now) ? DateTime.Now : taskDate.Date
            };

            dates.Add(newTask);
            writeTasksJson(dates);

            Navigation.PopAsync();
        }
    }
}