using Abscence.Beans;
using Abscence.Model;
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
    public partial class FormCourse : ContentPage
    {
        SQLiteAsyncConnection connection;
        Teacher teacher;
        public FormCourse(Teacher teacher)
        {
            InitializeComponent();
            this.teacher = teacher;
           
        }

        protected async override void OnAppearing()
        {
            backImage.Source = ImageSource.FromResource("Abscence.Images.yellow.jpg");
            courseLogo.Source = ImageSource.FromResource("Abscence.Images.Icons.course.png");
            connection = DependencyService.Get<ISQLiteDB>().GetConnection();
            await connection.CreateTableAsync<Student>();
            await connection.CreateTableAsync<Course>();
        }

        private async void addCourse_Clicked(object sender, EventArgs e)
        {
            string nameCourse = courseName.Text;
           
            if(fil.SelectedIndex >= 0 && !String.IsNullOrWhiteSpace(nameCourse))
            {
                string filiere = fil.Items[fil.SelectedIndex];
                Course course = new Course { CourseName = nameCourse, Filiere = filiere, IdTeacher = teacher.IdTeacher };
                await connection.InsertAsync(course);
                await Navigation.PopAsync();
                
            }
            else
            {
              
                await DisplayAlert("Alert", "Please fill out the required fields", "Ok");
            
            }
        }
    }
}