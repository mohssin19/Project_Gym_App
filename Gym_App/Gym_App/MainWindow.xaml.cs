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
        static List<User> Users = new List<User>();
        static List<HealthRecord> HealthRecords = new List<HealthRecord>();

        public static int CurrentUserId;
        public static int CurrentHealthRecordId;
        public static double CurrentAge;
        public static double CurrentWeight;
        public static double CurrentHeight;
        public static double CurrentBMI;

        public static string[] UN = new string[3] { "admin", "test", "user" };
        public static string PW = "password123";

        public MainWindow()
        {
            InitializeComponent();

            // BMI & BMR Text Info Hidden
            BMI_INFO.Visibility = Visibility.Hidden;
            BMR_INFO.Visibility = Visibility.Hidden;
            lbl_Kcal_Info.Visibility = Visibility.Hidden;

            // Manual Tab Control On main window
            //Tab1.IsSelected = true;
            //Tab2.IsSelected = false;
            Tab0.IsSelected = true;

            // Loading List on intialize
            //List<object> allRecords = new List<object>();
            
            using (var db = new GymAppDBEntities())
            {
                var allRecords = db.Users.Join(db.HealthRecords, u => u.UserId, hr => 
                hr.UserId, (u, hr) => new { u, hr }).Select(ar => 
                new {UserId = ar.u.UserId, 
                    FirstName = ar.u.FirstName, LastName = ar.u.LastName, 
                    Address = ar.u.Address, Email = ar.u.Email, TelephoneNumber = ar.u.TelephoneNumber,
                    HealthRecordId = ar.hr.HealthRecordId,
                    Age = ar.hr.Age, Weight = ar.hr.Weight, Height = ar.hr.Height,
                    BMI = ar.hr.BMI, BMR = ar.hr.BMR,
                })
                .ToList();
                //Users = db.Users.ToList();
                //HealthRecords = db.HealthRecords.ToList();
                AllMembersView.ItemsSource = allRecords;
            }

            // manual method
            //rabbits.ForEach(rabbit => ListBoxRabbits.Items.Add(rabbit))

         
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            Tab2.IsSelected = true;

            using (var dbc = new GymAppDBEntities())
            {
                //adding a user
                var userToAdd = new User();
                userToAdd.FirstName = txt_FirstName.Text;
                userToAdd.LastName = txt_LastName.Text;
                userToAdd.Address = txt_Address.Text;
                userToAdd.Email = txt_Email.Text;
                userToAdd.TelephoneNumber = txt_Mobile.Text;

                
                //adding a health record
                var healthInfoToAdd = new HealthRecord();
                healthInfoToAdd.UserId = userToAdd.UserId;
                healthInfoToAdd.Age = int.Parse(txt_Age.Text);
                healthInfoToAdd.Height = decimal.Parse(txt_Height.Text);
                healthInfoToAdd.Weight = decimal.Parse(txt_Weight.Text);

                //update db
                dbc.Users.Add(userToAdd);
                dbc.HealthRecords.Add(healthInfoToAdd);
                dbc.SaveChanges();

                //saving locally
                CurrentHealthRecordId = healthInfoToAdd.HealthRecordId;
                CurrentUserId = userToAdd.UserId;
                CurrentAge = (double)healthInfoToAdd.Age;
                CurrentWeight = (double)healthInfoToAdd.Weight;
                CurrentHeight = (double)healthInfoToAdd.Height;

                MessageBox.Show($"User: {userToAdd.FirstName} , successfully added!");
            }

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
               
                User CurrentUser = dbc.Users.Where(u => u.UserId == CurrentUserId).FirstOrDefault<User>();
                HealthRecord CurrentRecord = dbc.HealthRecords.Where(r => r.HealthRecordId == CurrentHealthRecordId).FirstOrDefault<HealthRecord>();


                // BMI CALUCULATIONS
                double bmi = Math.Round(((CurrentWeight / CurrentHeight / CurrentHeight) * 10000), 2);
                CurrentRecord.BMI = (decimal)bmi;
                BMI_Value.Text = CurrentRecord.BMI.ToString();


                // BMR Calculation Formula
                if (rdo_Female.IsChecked == true)
                {
                    double bmr = Math.Round(447.593 + (9.247 * CurrentWeight) + (3.098 * CurrentHeight) - (4.330 * CurrentAge), 2);
                    CurrentRecord.BMR = (decimal)bmr;
                    BMR_Value.Text = CurrentRecord.BMR.ToString();
                }
                else if (rdo_Male.IsChecked == true)
                {
                    double bmr = Math.Round(88.362 + (13.397 * CurrentWeight) + (4.799 * CurrentHeight) - (5.677 * CurrentAge), 2);
                    CurrentRecord.BMR = (decimal)bmr;
                    BMR_Value.Text = CurrentRecord.BMR.ToString();
                }

               
                dbc.SaveChanges();
                MessageBox.Show($"User: {CurrentUser.FirstName} , health calculations added!");
                Tab3.IsSelected = true;
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

        private void lbl_BMR_MouseEnter(object sender, MouseEventArgs e)
        {
            BMR_INFO.Visibility = Visibility.Visible;
        }

        private void lbl_BMR_MouseLeave(object sender, MouseEventArgs e)
        {
            BMR_INFO.Visibility = Visibility.Hidden;
        }


        //----------------------------- ALL KCAL FUNCTIONS-------------------------------//
        public double CalculateKcal(decimal cal)
        {
            using (var dbc = new GymAppDBEntities())
            {

                HealthRecord CurrentRecord = dbc.HealthRecords.Where(r => r.HealthRecordId == CurrentHealthRecordId).FirstOrDefault<HealthRecord>();

                if (CurrentRecord.BMR != null)
                {
                    double Kcal = (double)(CurrentRecord.BMR * cal);
                    CurrentRecord.KCAL = (decimal)Kcal;
                    return Kcal;
                }

            }
            return 0;
        }
        public void SetKcalText(string Kcal)
        {
            txt_Kcal.Text = Kcal.ToString();

        }
        private void rdo_Opt1_Checked(object sender, RoutedEventArgs e)
        {
            double Kcal = CalculateKcal(1.2m);
            SetKcalText(Kcal.ToString());
        }
        private void rdo_Opt2_Checked(object sender, RoutedEventArgs e)
        {
            double Kcal = CalculateKcal(1.375m);
            SetKcalText(Kcal.ToString());
        }

        private void rdo_Opt3_Checked(object sender, RoutedEventArgs e)
        {
            double Kcal = CalculateKcal(1.55m);
            SetKcalText(Kcal.ToString());
        }

        private void rdo_Opt4_Checked(object sender, RoutedEventArgs e)
        {
            double Kcal = CalculateKcal(1.725m);
            SetKcalText(Kcal.ToString());
        }

        private void rdo_Opt5_Checked(object sender, RoutedEventArgs e)
        {
            double Kcal = CalculateKcal(1.9m);
            SetKcalText(Kcal.ToString());
        }

        private void lbl_Kcal_MouseEnter(object sender, MouseEventArgs e)
        {
            lbl_Kcal_Info.Visibility = Visibility.Visible;
        }

        private void lbl_Kcal_MouseLeave(object sender, MouseEventArgs e)
        {
            lbl_Kcal_Info.Visibility = Visibility.Hidden;
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Username.Text == UN[0] || txt_Username.Text == UN[1] || txt_Username.Text == UN[2])
            {
                if (txt_Password.Text == PW)
                {
                    MainMenuTab.IsSelected = true;
                }
                else
                {
                    MessageBox.Show("Username name or password is incorrect");
                    txt_Username.Focus();
                }
            }
        }
    }
}

