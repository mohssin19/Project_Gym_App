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
            // BMI & BMR Text Info Hidden
            BMI_INFO.Visibility = Visibility.Hidden;
            BMR_INFO.Visibility = Visibility.Hidden;
            // Manual Tab Control On main window
            Tab1.IsSelected = true;
            Tab2.IsSelected = false;
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            Tab2.IsSelected = true;
        }



        private void btn_generateId_Click(object sender, RoutedEventArgs e)
        {
           
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
                double bmi = Math.Round((((double)healthInfoToAdd.Weight / (double)healthInfoToAdd.Height / (double)healthInfoToAdd.Height) * 10000),2);
                healthInfoToAdd.BMI = (decimal)bmi;
                BMI_Value.Text = healthInfoToAdd.BMI.ToString();

                // BMR Calculation Formula


                if (rdo_Female.IsChecked == true)
                {
                    double bmr = Math.Round(447.593 + (9.247 * (double)healthInfoToAdd.Weight) + (3.098 * (double)healthInfoToAdd.Height) - (4.330 * (double)healthInfoToAdd.Age), 2);
                    healthInfoToAdd.BMR = (decimal)bmr;
                    BMR_Value.Text = healthInfoToAdd.BMR.ToString();
                }
                else if (rdo_Male.IsChecked == true)
                {
                    double bmr = Math.Round(88.362 + (13.397 * (double)healthInfoToAdd.Weight) + (4.799 * (double)healthInfoToAdd.Height) - (5.677 * (double)healthInfoToAdd.Age), 2);
                    healthInfoToAdd.BMR = (decimal)bmr;
                    BMR_Value.Text = healthInfoToAdd.BMR.ToString();
                }
                else if (rdo_Female.IsChecked == false && rdo_Male.IsChecked == false)
                {
                    MessageBox.Show("Invalid! Please select Male or Female Options");
                    txt_Age.Clear();
                    txt_Age.Focus();
                    txt_Height.Clear();
                    txt_Weight.Clear();
                    BMI_Value.Clear();
                    BMR_Value.Clear();

                    //update db
                    dbc.Users.Add(userToAdd);
                    dbc.HealthRecords.Add(healthInfoToAdd);
                    dbc.SaveChanges();
                    MessageBox.Show($"User: {userToAdd.FirstName} , successfully added!");
                }
            }
        }

        private void lbl_BMI_MouseEnter(object sender, MouseEventArgs e)
        {
            BMI_INFO.Visibility = Visibility.Visible;
        }

        private void lbl_BMI_MouseLeave(object sender, MouseEventArgs e)
        {
            BMI_INFO.Visibility = Visibility.Hidden;

        }

        private void rdo_Female_Checked(object sender, RoutedEventArgs e)
        {
     
            
        }
    }
}
