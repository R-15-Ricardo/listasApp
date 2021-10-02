using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static listas.Readers;

namespace listas
{
    public partial class MainPage : ContentPage
    {
        Dictionary<string, bool> checkList = new Dictionary<string, bool>();
        List<ListItem> dates = new List<ListItem>();
        public MainPage()
        {
            InitializeComponent();

            dates = readTasksJson();

            drawTasks(dates);
        }
        protected override void OnAppearing()
        {
            dates = readTasksJson();
            drawTasks(dates);
        }

        public void drawTasks(List<ListItem> dates)
        {
            taskContainer.Children.Clear();
            checkList.Clear();

            if (dates == null)
            {
                Label nothing = new Label() { Text = "¡Estas al dia con todo!", TextColor = Color.Gray, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand };
                taskContainer.Children.Add(nothing);
                return;
            }
            else if (dates.Count == 0)
            {
                Label nothing = new Label() { Text = "¡Estas al dia con todo!", TextColor = Color.Gray, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand };
                taskContainer.Children.Add(nothing);
                return;
            }

            foreach (var item in dates)
            {
                Frame task = new Frame();

                Grid tasklayout = new Grid
                {
                    RowDefinitions = {
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    },
                    ColumnDefinitions = {
                        new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    }
                };

                StackLayout taskInfo = new StackLayout();

                taskInfo.Children.Add(new Label { Text = item.title, FontAttributes = FontAttributes.Bold, FontSize = 15, Margin = new Thickness(15, 0, 0, 5) });
                taskInfo.Children.Add(new Label { Text = item.description, Margin = new Thickness(15, 0, 0, 10) });
                if (DateTime.Now > item.date)
                    taskInfo.Children.Add(new Label { Text = $"Fecha limite: {item.date.ToString("dd/MM/yyyy")}", Margin = new Thickness(15, 0, 0, 10), TextColor = Color.Red });
                else
                    taskInfo.Children.Add(new Label { Text = $"Fecha limite: {item.date.ToString("dd/MM/yyyy")}", Margin = new Thickness(15, 0, 0, 10) });


                tasklayout.Children.Add(taskInfo, 0, 0);

                CheckBox doneMarker = new CheckBox();
                checkList.Add(item.title, false);

                doneMarker.CheckedChanged += (sender, e) =>
                {
                    if (doneMarker.IsChecked)
                    {
                        checkList[item.title] = true;
                    }
                    else
                    {
                        checkList[item.title] = false;
                    }
                };


                tasklayout.Children.Add(doneMarker, 1, 0);

                task.Content = tasklayout;
                task.Margin = new Thickness(10, 5, 10, 2);
                task.Padding = 10;
                task.BackgroundColor = Color.White;

                taskContainer.Children.Add(task);
            }
        }

        private void addBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTask());
        }

        private void Flusher_Clicked(object sender, EventArgs e)
        {
            List<ListItem> newTasts = new List<ListItem>();

            foreach (ListItem task in dates)
            {
                if (!checkList[task.title]) newTasts.Add(task);
            }
                

            dates = newTasts;

            writeTasksJson(dates);
            drawTasks(dates);
        }
    }
}
