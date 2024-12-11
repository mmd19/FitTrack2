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

        //Konstruktor
        public UserDetailsWindow(User UserSignedIn)
        {
            InitializeComponent();
            DataContext = this;
            this.UserSignedIn = UserSignedIn;
            UsernametxtBlock.Text = UserSignedIn.Username;
            Countries();



        }

        //Metod för att kunna välja land i combobox
        private void Countries()
        {
            List<string> countries = new List<string>
            {
                "Sweden", "Denmark", "Norway", "Finland"
            };
            NewCountryComboBox.ItemsSource = countries;
            NewCountryComboBox.SelectedItem = UserSignedIn.Country;
        }

        //Hanterar spara knappen
        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            bool hasError = false;

            //Kollar om användarnamnet är ifyllt
            if (!string.IsNullOrEmpty(NewUsernametxtBox.Text))
            {
                //Kollar längd på användarnamnet
                if (NewUsernametxtBox.Text.Length < 3)
                {
                    MessageBox.Show("Username must be at least 3 letter", "Message");
                    hasError = true;
                }
                else
                {
                    //Kollar om användarnamnet finns
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
                        //Uppdaterar användarnamn
                        UserSignedIn.Username = NewUsernametxtBox.Text;
                    }


                }
            }

            //Kollar om lösenord är ifyllt
            if (!string.IsNullOrEmpty(NewPasswordtxtBox.Text))
            {
                //Kollar om lösenord matchar
                if (NewPasswordtxtBox.Text != ConfirmNewPasswordtxtBox.Text)
                {
                    MessageBox.Show("Password do not match", "Message");
                    hasError = true;
                }

                //Kollar längd på lösenord
                else if (NewPasswordtxtBox.Text.Length < 5)
                {
                    MessageBox.Show("Password must be at least 5 character", "Message");
                    hasError = true;
                }
                else
                {
                    //Uppdaterar lösenord
                    UserSignedIn.Password = NewPasswordtxtBox.Text;
                }
            }

          // Kollar om fälten är tomma
            if (string.IsNullOrEmpty(NewUsernametxtBox.Text) &&
                string.IsNullOrEmpty(NewPasswordtxtBox.Text) &&
                string.IsNullOrEmpty(ConfirmNewPasswordtxtBox.Text) &&
                string.IsNullOrEmpty(NewCountryComboBox.Text))
            {

                MessageBox.Show("Please, fill in the field you want to change.", "Message");
                hasError = true;
            }

            //Uppdaterar land
            UserSignedIn.Country = NewCountryComboBox.Text;
         

           // Om inga fel hittas, sparas ändringarna och öppnar workoutwindow
           if (!hasError)
           {
                MessageBox.Show("Saved", "Message");
                WorkoutsWindow workoutWindow = new WorkoutsWindow(UserSignedIn);
                workoutWindow.Show();
                this.Close();
           }

        }

        //Hanterar avbryt knappen
        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            //Stänger ner detailwindow och öppnar workoutwindow
            WorkoutsWindow workoutWindow = new WorkoutsWindow(UserSignedIn);
            workoutWindow.Show();
            this.Close();
            
            

        }
    }
}
