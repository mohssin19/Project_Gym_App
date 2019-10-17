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
using System.Windows.Shapes;

namespace Gym_App
{
    /// <summary>
    /// Interaction logic for HealthWindow.xaml
    /// </summary>
    public partial class HealthWindow : Window
    {
        public HealthWindow()
        {
            InitializeComponent();
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            using (var dbc = new GymAppDBEntities())
            {
                
                var userHealthRecord = new HealthRecord();
                userHealthRecord.UserId = MainWindow.CurrentUserId;
                userHealthRecord.Age = int.Parse(txt_Age.Text);
                dbc.HealthRecords.Add(userHealthRecord);
                dbc.SaveChanges();
                //txt_LastName.Text = int.Parse(txt_ClientId.Text).ToString();



            }
        }
    }
}
