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
using FitTrack2.Klasser;

namespace FitTrack2
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        //Konstruktor
        public RegisterWindow()
        {
            InitializeComponent();
            Countries();
            SecurityQuestion();
            DataContext = this;
        }
        
        //Metod för att välja land
        private void Countries()
        {
            List<string> countries = new List<string> 
            { 
                "Sweden", "Denmark", "Norway", "Finland" 
            };
            CountryComboBox.ItemsSource = countries;
        }
        
        //Metod för att välja säkerhetsfråga
        private void SecurityQuestion()
        {
            List<string> securityQuestion = new List<string>
            {
                "What is your favorite color?",
                "What is your favorite book?",
                "What is the name of your childhood best friend?" 
            };
            SecurityQuestionComboBox.ItemsSource = securityQuestion;
        }

        //Hanterar bekräftelse knapp
        private void Confirmbtn_Click(object sender, RoutedEventArgs e)
        {
            string username = RegisterUsernametxtBox.Text;
            string password = RegisterPasswordtxtBox.Text;
            string confirmPassword = ConfirmPasswordtxtBox.Text;
            string country = CountryComboBox.Text;
            string securityQuestion = SecurityQuestionComboBox.Text;
            string securityAnswer = SecurityAnswertxtBox.Text;

            //Kollar om användarnamnet finns
            if (UserManager.Instance.UserExist(username))
            {
                MessageBox.Show("Username already exist", "Message");
            }

            //Kollar om något fält är tomt
            else if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword) ||
                string.IsNullOrEmpty(country) ||
                string.IsNullOrEmpty(securityQuestion) ||
                string.IsNullOrEmpty(securityAnswer))
            {
                MessageBox.Show("Please, fill in all fields", "Message");
            }

            //Kollar om lösenord och bekräfta lösenord matchar
            else if (password != confirmPassword)
            {
                MessageBox.Show("Password do not match", "Message");


            }

            //Om allt är ifyllt korrekt och användarnamnet inte finns så läggs en ny användare till
            else
            {
                UserManager.Instance.RegisteredUsers.Add(new User(username, password, country, securityQuestion, securityAnswer));
                MessageBox.Show("Registration was successful", "Message");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();

            }
        }
    }
}
