using Abscence.Beans;
using Abscence.persistence;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Abscence
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        List<Student> students;
        Teacher teacher;
        SQLiteAsyncConnection connection;

        public SearchPage(Teacher teacher)
        {
            InitializeComponent();
            this.teacher = teacher;
        }

        protected async override void OnAppearing()
        {
            backImage.Source = ImageSource.FromResource("Abscence.Images.yellow.jpg");
            connection = DependencyService.Get<ISQLiteDB>().GetConnection();
            await connection.CreateTableAsync<Student>();
         
            students = await connection.Table<Student>().Where(c => c.IdTeacher == teacher.IdTeacher).ToListAsync();
            listView.ItemsSource = students;
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = e.NewTextValue;
            if (!String.IsNullOrWhiteSpace(str))
            {
                listView.ItemsSource = (from s in students
                                        where s.LastName.ToLower().StartsWith(str.ToLower()) || s.FirstName.ToLower().StartsWith(str.ToLower())
                                        select s).ToList();
            }
            else
            {
                listView.ItemsSource = students;
            }
        }

        private async void student_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Student student = e.SelectedItem as Student;
            await Navigation.PushAsync(new StudentAttendance(student));
            searchBar.Text = "";
        }
    }
}