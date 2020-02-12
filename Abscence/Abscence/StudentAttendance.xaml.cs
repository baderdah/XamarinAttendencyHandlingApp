using Abscence.Beans;
using Abscence.persistence;
using SQLite;
using System;
using System.Collections.Generic;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Abscence
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentAttendance : ContentPage
    {
        Student student;
        SQLiteAsyncConnection connection;
        List<Attendance> attendances;
        public StudentAttendance(Student student)
        {

            InitializeComponent();
            this.student = student;
        }

        protected override async void OnAppearing()
        {
            logoStudent.Source = ImageSource.FromResource("Abscence.Images.Icons.student.png");
            bgImage.Source = ImageSource.FromResource("Abscence.Images.yellow.jpg");
            connection = DependencyService.Get<ISQLiteDB>().GetConnection();
            await connection.CreateTableAsync<Attendance>();
            attendances = await connection.Table<Attendance>().Where(att => att.IdStudent == student.IdStudent).ToListAsync();
            firstName.Text = student.FirstName;
            lastName.Text = student.LastName;
            option.Text = student.Filiere;
            cne.Text = student.Cne.ToString();
            nbAbscences.Text = attendances.Count.ToString();
        }

        private async void Details_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AttendanceDetail(student));
        }
    }
}