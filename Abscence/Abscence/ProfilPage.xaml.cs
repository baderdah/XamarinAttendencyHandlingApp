using Abscence.Beans;
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
    public partial class ProfilPage : ContentPage
    {
        Teacher teacher;
        public ProfilPage(Teacher teacher)
        {
            InitializeComponent();
            this.teacher = teacher;
        }

        protected override void OnAppearing()
        {
            InitialiseIcons();
            
        }

        private void InitialiseIcons()
        {
            backImage.Source = ImageSource.FromResource("Abscence.Images.yellow.jpg");
            home.Source = ImageSource.FromResource("Abscence.Images.Icons.home.png");
            abscence.Source = ImageSource.FromResource("Abscence.Images.Icons.abscence.png");
            addstudent.Source = ImageSource.FromResource("Abscence.Images.Icons.database.png");
            addcourse.Source = ImageSource.FromResource("Abscence.Images.Icons.course.png");
            search.Source = ImageSource.FromResource("Abscence.Images.Icons.search.png");
            logout.Source = ImageSource.FromResource("Abscence.Images.Icons.logout.png");
            OnTapIcons();
        }

        private void OnTapIcons()
        {
            var tapLogout = new TapGestureRecognizer();
            tapLogout.Tapped += (s, e) => {
                Navigation.PushAsync(new LoginPage());
            };
            logout.GestureRecognizers.Add(tapLogout);

            var tapSearch = new TapGestureRecognizer();
            tapSearch.Tapped += (s, e) => {
                Navigation.PushAsync(new SearchPage(teacher));
            };
            search.GestureRecognizers.Add(tapSearch);

            var tapAbscence = new TapGestureRecognizer();
            tapAbscence.Tapped += (s, e) => {
                Navigation.PushAsync(new ListAbscence(teacher));
            };
            abscence.GestureRecognizers.Add(tapAbscence);

            var tapCourse = new TapGestureRecognizer();
            tapCourse.Tapped += (s, e) => {
                Navigation.PushAsync(new FormCourse(teacher));
            };
            addcourse.GestureRecognizers.Add(tapCourse);

            var tapStudent = new TapGestureRecognizer();
            tapStudent.Tapped += (s, e) => {
                Navigation.PushAsync(new FormStudent(teacher));
            };
            addstudent.GestureRecognizers.Add(tapStudent);
        }

        private async void Abscence_Clicked(object sender, EventArgs e)
        {
            if (e != null)
                await Navigation.PushAsync(new ListAbscence(teacher));
        }

        private async void Search_Clicked(object sender, EventArgs e)
        {
            if (e != null)
                await Navigation.PushAsync(new SearchPage(teacher));
        }

        private async void AddCourse_Clicked(object sender, EventArgs e)
        {
            if (e != null)
                await Navigation.PushAsync(new FormCourse(teacher));
        }

        private async void AddStudent_Clicked(object sender, EventArgs e)
        {
            if(e != null)
                await Navigation.PushAsync(new FormStudent(teacher));
        }

        private async void LogOut_Clicked(object sender, EventArgs e)
        {
            if (e != null)
                await Navigation.PushAsync(new LoginPage());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}