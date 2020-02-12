using Abscence.Beans;
using Abscence.Controllers;
using Abscence.persistence;
using SQLite;
using System;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Abscence
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormStudent : ContentPage
    {
        Teacher teacher;
        SQLiteAsyncConnection connection;
        public FormStudent(Teacher teacher)
        {
            InitializeComponent();
            this.teacher = teacher;           
        }

        protected async override void OnAppearing()
        {
            backImage.Source = ImageSource.FromResource("Abscence.Images.yellow.jpg");
            logoStudent.Source = ImageSource.FromResource("Abscence.Images.Icons.student.png");
            connection = DependencyService.Get<ISQLiteDB>().GetConnection();
            await connection.CreateTableAsync<Student>();
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            InputController inputController = new InputController(cne.Text, lastName.Text, firstName.Text, mail.Text, phoneNumber.Text);
            if (!String.IsNullOrWhiteSpace(inputController.Msg))
            {
                await DisplayAlert("Error", inputController.Msg, "OK");
            }
            else
            {
                Student student = new Student
                {
                    Cne = Convert.ToInt32(cne.Text),
                    Filiere = filiere.Items[filiere.SelectedIndex],
                    FirstName = firstName.Text,
                    LastName = lastName.Text,
                    Mail = mail.Text,
                    Tel = phoneNumber.Text,
                    IdTeacher = teacher.IdTeacher
                };
                await connection.InsertAsync(student);
                await DisplayAlert("Info", "student added succesfully", "ok");
                await Navigation.PushAsync(new ProfilPage(teacher));
            }
        }
    }
}