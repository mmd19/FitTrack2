using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FitTrack2.Klasser;
using FitTrack2.Windows;

namespace FitTrack2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        
        private void MainSignInbtn_Click(object sender, RoutedEventArgs e)
        {
            string Username = MainUsernametxtBox.Text;
            string Password = MainPasswordtxtBox.Text;

            User foundUser = UserManager.Instance.GetUsers(Username);
            if (foundUser != null && foundUser.Password == Password && foundUser.Username == Username)
            {
                foundUser.SignIn();
               
                if (foundUser is AdminUser)
                {
                    ((AdminUser) foundUser).ManageAllWorkouts();
                }
                WorkoutsWindow workoutsWindow = new WorkoutsWindow(foundUser);
                workoutsWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong username or password", "Message");
            }
        }

        private void MainRegisterbtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Application.Current.MainWindow.Close();
        }
    }
}