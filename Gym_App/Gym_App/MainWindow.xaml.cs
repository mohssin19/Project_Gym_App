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
                    userToAdd.UserId = txt_ClientId.Text;
                    CurrentUserId = txt_ClientId.Text;
                    dbc.Users.Add(userToAdd);
                    dbc.SaveChanges();
                    //txt_LastName.Text = int.Parse(txt_ClientId.Text).ToString();

                }

            }
            var HealthWindow = Window(); //create your new form.
            
            HealthWindow.Show(); //show the new form.
            this.Close(); //only if you want to close the current form.
        }

        private void btn_generateId_Click(object sender, RoutedEventArgs e)
        {
            int randomNumber = random.Next(0, 100);
            txt_ClientId.Text = randomNumber.ToString();
        }
    }
}
