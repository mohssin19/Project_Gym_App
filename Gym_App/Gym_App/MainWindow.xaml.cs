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
        public static string CurrentUserId = null; 
        

        public MainWindow()
        {
            InitializeComponent();
            Tab1.IsSelected = true;
            Tab2.IsSelected = false;
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            Tab2.IsSelected = true;
        }

        

        private void TabController_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void btn_SaveAll_Click(object sender, RoutedEventArgs e)
        {
            using (var dbc = new GymAppDBEntities())
            {

                var userToAdd = new User();
                userToAdd.FirstName = txt_FirstName.Text;
                userToAdd.LastName = txt_LastName.Text;
                userToAdd.Address = txt_Address.Text;
                userToAdd.Email = txt_Email.Text;
                userToAdd.TelephoneNumber = txt_Mobile.Text;
                //userToAdd.UserId = txt_ClientId.Text;

                var healthInfoToAdd = new HealthRecord();
                healthInfoToAdd.UserId = userToAdd.UserId;
                healthInfoToAdd.Age = int.Parse(txt_Age.Text);
                healthInfoToAdd.Height = decimal.Parse(txt_Height.Text);
                healthInfoToAdd.Weight = decimal.Parse(txt_Weight.Text);

                // BMI Calculation Formula
                //healthInfoToAdd.BMI = (healthInfoToAdd.Weight / healthInfoToAdd.Height / healthInfoToAdd.Height) * 10000;
                //BMI_Value.Text = healthInfoToAdd.BMI.ToString();

                //update db
                dbc.Users.Add(userToAdd);
                dbc.HealthRecords.Add(healthInfoToAdd);
                dbc.SaveChanges();

                //MessageBox.Show($"User: {userToAdd.FirstName} , successfully added!");
            }
        }
    }
}
