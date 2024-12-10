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
        
        //Hanterar inloggnings knapp
        private void MainSignInbtn_Click(object sender, RoutedEventArgs e)
        {
            string Username = MainUsernametxtBox.Text;
            string Password = MainPasswordtxtBox.Text;

            //Hämtar användare från UserManager beroende på användarnamn
            User foundUser = UserManager.Instance.GetUsers(Username);
           
            //Kollar om användaren finns samt ifall lösenord matchar
            if (foundUser != null && foundUser.Password == Password && foundUser.Username == Username)
            {
                foundUser.SignIn();
               
                //Kollar om användaren är admin
                if (foundUser is AdminUser)
                {
                    //Admin hanterar alla träningspass
                    ((AdminUser) foundUser).ManageAllWorkouts();
                }

                //Öppnar nytt fönster och stänger ner nuvarande
                WorkoutsWindow workoutsWindow = new WorkoutsWindow(foundUser);
                workoutsWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong username or password", "Message");
            }
        }

        //Hanterar Registrering knapp
        private void MainRegisterbtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Application.Current.MainWindow.Close();
        }
    }
}