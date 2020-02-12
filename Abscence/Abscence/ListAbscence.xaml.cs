using Abscence.Beans;
using Abscence.Model;
using Abscence.persistence;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Abscence
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListAbscence : ContentPage
    {

        ObservableCollection<Student> students;
        List<Course> courses;
        Teacher teacher;
        SQLiteAsyncConnection connection;

        public ListAbscence(Teacher teacher)
        {
            
            InitializeComponent();
            bgImage.Source = ImageSource.FromResource("Abscence.Images.yellow.jpg");
            this.teacher = teacher;
          
        }

        protected async override void OnAppearing()
        {
            connection = DependencyService.Get<ISQLiteDB>().GetConnection();
            await connection.CreateTableAsync<Student>();
            await connection.CreateTableAsync<Course>();
            await connection.CreateTableAsync<Attendance>();

            courses = await connection.Table<Course>().Where(c => c.IdTeacher == teacher.IdTeacher).ToListAsync();
            students = new ObservableCollection<Student>(await connection.Table<Student>().Where(c => c.IdTeacher == teacher.IdTeacher).ToListAsync());

            if (students != null && courses != null)
            {
                waiting.IsRunning = false;
                Studentslist.ItemsSource = students;               
            }
           
        }

        private void Filiere_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filiere = fil.Items[fil.SelectedIndex];
            Studentslist.ItemsSource = students.Where(s => s.Filiere.Equals(filiere));
            crs.Items.Clear();
            foreach(Course course in courses.Where(c => c.Filiere.Equals(filiere)))
            {
                crs.Items.Add(course.CourseName);
            }
        }

        private async void BacktoMenu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Student_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           Student student = e.SelectedItem as Student;
            if(student != null)
            {
                var abscent = await DisplayAlert("Alert", "Mark student: " + student.FirstName + " " + student.LastName + " as abscent", "Yes", "No");
                if (abscent == true)
                {

                    if (fil.SelectedIndex >= 0 && crs.SelectedIndex >= 0)
                    {
                        Attendance attendance = new Attendance { Day = day.Date.ToShortDateString(), Start = start.Time.ToString(), End = end.Time.ToString(), Course = crs.Items[crs.SelectedIndex], IdStudent = student.IdStudent };
                        await connection.InsertAsync(attendance);
                        await DisplayAlert("", attendance.Day, "ok");
                        Studentslist.SelectedItem = null;
                    }
                    else
                    {
                        await DisplayAlert("Error", "Please fill out the options above", "ok");
                    }

                }
                else
                {
                    Studentslist.SelectedItem = null;
                }
            }
           
        }

        private async void DeleteStd_Clicked(object sender, EventArgs e)
        {
            Student student = (sender as MenuItem).CommandParameter as Student;
            students.Remove(student);
            await connection.DeleteAsync(student);
        }
    }
}