using Abscence.Beans;
using Abscence.Controllers;
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

    public partial class SignUpPage : ContentPage
    {
        SQLiteAsyncConnection connection;

        public SignUpPage()
        {
            InitializeComponent();
            
        }

        protected async override  void OnAppearing()
        {
            backImage.Source = ImageSource.FromResource("Abscence.Images.yellow.jpg");
            professorLogo.Source = ImageSource.FromResource("Abscence.Images.Icons.professor.png");
            connection = DependencyService.Get<ISQLiteDB>().GetConnection();
            await connection.CreateTableAsync<Student>();
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            InputController inputController = new InputController(userName.Text, password.Text, passwordConfirmation.Text);
            string errorMsg = inputController.Msg;

            if (!String.IsNullOrWhiteSpace(errorMsg))
            {
                await DisplayAlert("Error", errorMsg, "Ok");
            }
            else
            {
                Teacher teacher = new Teacher { UserName = userName.Text, Password = password.Text };
                await connection.InsertAsync(teacher);
                await DisplayAlert("Congratulation", "Successfully registred", "Login");
                await Navigation.PopAsync();

            }
        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            password.Text = "";
            passwordConfirmation.Text = "";
            userName.Text = "";
        }

    }
}