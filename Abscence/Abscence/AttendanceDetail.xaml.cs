using Abscence.Beans;
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
    public partial class AttendanceDetail : ContentPage
    {
        Student student;
        ObservableCollection<Attendance> abscences;
        List<Attendance> attendances;
        SQLiteAsyncConnection connection;
        public AttendanceDetail(Student student)
        {
            InitializeComponent();
            this.student = student;
        }

        protected async override void OnAppearing()
        {
            bgImage.Source = ImageSource.FromResource("Abscence.Images.yellow.jpg");
            connection = DependencyService.Get<ISQLiteDB>().GetConnection();
            await connection.CreateTableAsync<Attendance>();
            attendances = await connection.Table<Attendance>().Where(att => att.IdStudent == student.IdStudent).ToListAsync();
            if(attendances == null)
            {
                await DisplayAlert("Info", "No correspondant attendance", "ok");
            }
            else
            {
                var temp = from a in attendances
                           orderby a.IdAbscence descending
                           select a;
                if(temp == null)
                    await DisplayAlert("Info", "No correspondant attendance", "ok");
                else
                {
                    abscences = new ObservableCollection<Attendance>(temp);
                    fullName.Text = student.FirstName + " " + student.LastName;
                    listView.ItemsSource = abscences;
                }
                
            }
            
            //await DisplayAlert(attendances.ElementAt(3).Start+" "+attendances.ElementAt(3).End, attendances.ElementAt(3).Day, "ok");
           
        }

        private async void DeleteAbscence_Clicked(object sender, EventArgs e)
        {
            Attendance attendance = (sender as MenuItem).CommandParameter as Attendance;
            await DisplayAlert("Sure about this removal", attendance.IdAbscence + " " + attendance.Course, "ok");
           if(attendance.IdAbscence != 0)
            {
                attendances.Remove(attendance);
                await connection.DeleteAsync(attendance);
            }
            else
            {
                await DisplayAlert("Warning", "Suppression cannot be done", "ok");
            }
        }
    }
}