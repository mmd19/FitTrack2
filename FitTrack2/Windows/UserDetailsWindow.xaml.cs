using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace FitTrack2.Windows
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        public User UserSignedIn { get; set; }
        public UserDetailsWindow(User UserSignedIn)
        {
            InitializeComponent();
            DataContext = this;
            this.UserSignedIn = UserSignedIn;
            UsernametxtBlock.Text = UserSignedIn.Username;
            Countries();



        }

        private void Countries()
        {
            List<string> countries = new List<string>
            {
                "Sweden", "Denmark", "Norway", "Finland"
            };
            NewCountryComboBox.ItemsSource = countries;
            NewCountryComboBox.SelectedItem = UserSignedIn.Country;
        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            bool hasError = false;
            if (!string.IsNullOrEmpty(NewUsernametxtBox.Text))
            {
                if (NewUsernametxtBox.Text.Length < 3)
                {
                    MessageBox.Show("Username must be at least 3 letter", "Message");
                    hasError = true;
                }
                else
                {
                    foreach (User user in UserManager.Instance.RegisteredUsers)
                    {
                        if (user.Username.Equals(NewUsernametxtBox.Text))
                        {
                            MessageBox.Show("Username already taken");
                            hasError = true;
                        }
                    }
                    if (!hasError)
                    {
                        UserSignedIn.Username = NewUsernametxtBox.Text;
                    }


                }
            }
            if (!string.IsNullOrEmpty(NewPasswordtxtBox.Text))
            {
                if (NewPasswordtxtBox.Text != ConfirmNewPasswordtxtBox.Text)
                {
                    MessageBox.Show("Password do not match", "Message");
                    hasError = true;
                }
                else if (NewPasswordtxtBox.Text.Length < 5)
                {
                    MessageBox.Show("Password must be at least 5 character", "Message");
                    hasError = true;
                }
                else
                {
                    UserSignedIn.Password = NewPasswordtxtBox.Text;
                }
            }

          
            if (string.IsNullOrEmpty(NewUsernametxtBox.Text) &&
                string.IsNullOrEmpty(NewPasswordtxtBox.Text) &&
                string.IsNullOrEmpty(ConfirmNewPasswordtxtBox.Text) &&
                string.IsNullOrEmpty(NewCountryComboBox.Text))
            {

                MessageBox.Show("Please, fill in the field you want to change.", "Message");
                hasError = true;
            }

            UserSignedIn.Country = NewCountryComboBox.Text;
         

           
           if (!hasError)
           {
                MessageBox.Show("Saved", "Message");
                WorkoutsWindow workoutWindow = new WorkoutsWindow(UserSignedIn);
                workoutWindow.Show();
                this.Close();
           }

        }

        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            WorkoutsWindow workoutWindow = new WorkoutsWindow(UserSignedIn);
            workoutWindow.Show();
            this.Close();
            
            

        }
    }
}
