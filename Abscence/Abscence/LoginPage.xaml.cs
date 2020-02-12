using Abscence.Beans;
using Abscence.Controllers;
using Abscence.Model;
using Abscence.persistence;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Abscence
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        SQLiteAsyncConnection connection;
        DbManager manager;
        List<Teacher> teachers;

        public LoginPage()
        {
            InitializeComponent();
            
        }

        protected async override void OnAppearing()
        {
            logo.Source = ImageSource.FromResource("Abscence.Images.logo.png");
            backGrd.Source = ImageSource.FromResource("Abscence.Images.yellow.jpg");
            connection = DependencyService.Get<ISQLiteDB>().GetConnection();
            await connection.CreateTableAsync<Teacher>();
            teachers = await connection.Table<Teacher>().ToListAsync();
            manager = new DbManager(); 
        }

        async void Sign_Clicked(object sender, EventArgs e)
        {
            login.Text = "";
            password.Text = "";
            await Navigation.PushAsync(new SignUpPage());
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            InputController inputController = new InputController(login.Text, password.Text);
            string errorMsg = inputController.Msg;
            if (!String.IsNullOrWhiteSpace(errorMsg))
            {
                await DisplayAlert("Error", errorMsg, "Ok");
            }
            else
            {
                Teacher teacher = manager.checkLoginTeacher(login.Text, password.Text, teachers);
                login.Text = "";
                password.Text = "";
                if (teacher == null)
                {
                    await DisplayAlert("Info", "This account doesn't exist", "Ok");
                }
                else
                {
                    await Navigation.PushAsync(new ProfilPage(teacher));
                }
                
            }
            
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}