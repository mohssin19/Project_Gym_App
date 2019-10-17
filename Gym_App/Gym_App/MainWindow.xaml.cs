using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gym_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Random random = new Random();
        public static string CurrentUserId = null; 
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {

            using(var dbc = new GymAppDBEntities())
            {
                var existingUser = dbc.Users.Where(u => u.UserId.ToString() == txt_ClientId.Text).FirstOrDefault();
                if(existingUser == null)
                {
                    var userToAdd = new User();
                    userToAdd.FirstName = txt_FirstName.Text;
                    userToAdd.LastName = txt_LastName.Text;
                    userToAdd.Address = txt_Address.Text;
                    userToAdd.Email = txt_Email.Text;
                    userToAdd.TelephoneNumber = txt_Mobile.Text;
                    userToAdd.UserId = txt_ClientId.Text;

                    var healthInfoToAdd = new HealthRecord();
                    healthInfoToAdd.UserId = CurrentUserId;
                    healthInfoToAdd.Age = int.Parse(txt_Age.Text);
                    healthInfoToAdd.Height = int.Parse(txt_Height.Text);
                    healthInfoToAdd.Weight = int.Parse(txt_Weight.Text);

                    //saving current id to static to keep adding data in another table about this user
                    CurrentUserId = txt_ClientId.Text;

                    //update db
                    dbc.Users.Add(userToAdd);
                    dbc.HealthRecords.Add(healthInfoToAdd);
                    dbc.SaveChanges();

                }

            }
        }

        private void btn_generateId_Click(object sender, RoutedEventArgs e)
        {
            int randomNumber = random.Next(0, 100);
            txt_ClientId.Text = randomNumber.ToString();
        }

        private void TabController_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
