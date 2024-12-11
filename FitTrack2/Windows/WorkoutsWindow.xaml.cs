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

namespace FitTrack2.Windows
{
    /// <summary>
    /// Interaction logic for WorkoutsWindow.xaml
    /// </summary>
    public partial class WorkoutsWindow : Window
    {
        public User UserSignedIn { get; set; }
        
        //Konstruktor
        public WorkoutsWindow(User UserSignedIn)
        {
            InitializeComponent();
            DataContext = this;
            this.UserSignedIn = UserSignedIn;
            UsernametxtBlock.Text = UserSignedIn.Username;
            Workouts();
        }

        //Metod för användarnas träningspass i lista
        public void Workouts()
        {
            WorkoutList.ItemsSource = UserSignedIn.workout;
        }

        // Metod för att uppdatera listan efter ändringar
        public void UpdateWorkoutList()
        {
            WorkoutList.ItemsSource = null;
            WorkoutList.ItemsSource = UserSignedIn.workout;
        }

        //Hanterar användarens info, navigerar till userdetailwindow
        private void Userbtn_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow(UserSignedIn);
            userDetailsWindow.Show();
            this.Close();
        }

        //Hanterar logga ut knappen, navigerar tillbaka till mainwindow
        private void SignOutbtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }

        //Hanterar lägga till träningspass knappen, navigerar till addworkoutwindow
        private void AddWorkoutbtn_Click(object sender, RoutedEventArgs e)
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow(UserSignedIn.workout);
            addWorkoutWindow.Closed += (s, args) => UpdateWorkoutList(); // Uppdatera listan när fönstret stängs
            addWorkoutWindow.Show();

        }

        //Hanterar träningspassdetaljer knappen, navigerar till workoutdetailwindow
        private void Detailsbtn_Click(object sender, RoutedEventArgs e)
        {
            if (WorkoutList.SelectedItem is Workout selectedWorkout)
            {
                WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow(selectedWorkout, UserSignedIn);
                workoutDetailsWindow.Closed += (s, args) => UpdateWorkoutList(); // Uppdatera listan när fönstret stängs
                workoutDetailsWindow.Show();
                this.Close();
            }
            else
            {
                //Visar meddelandet om inget träningspass är valt
                MessageBox.Show("Please select workout", "Message"); 
            }

        }

        //Hanterar ta bort träningspass knappen
        private void RemoveWorkoutbtn_Click(object sender, RoutedEventArgs e)
        {

            if (WorkoutList.SelectedItem is Workout selectedWorkout)
            {
                //Ifall användaren är admin
                if (UserSignedIn is AdminUser)
                {
                    User founduser = null;

                    //Går igenom träningspass för att se vems
                    foreach (User user in UserManager.Instance.RegisteredUsers)
                    {
                        foreach (Workout workout in user.workout)
                        {
                            if (selectedWorkout.Equals(workout))
                            {
                                founduser = user;
                            }
                        }
                    }

                    //Om användare hittas, tas träningspasset bort
                    if (founduser != null)
                    {
                        founduser.workout.Remove(selectedWorkout);
                        UserSignedIn.workout.Remove(selectedWorkout);
                    }
                } else
                {
                    UserSignedIn.workout.Remove(selectedWorkout);

                }

                //Uppdaterar listan
                WorkoutList.ItemsSource = null;
                WorkoutList.ItemsSource = UserSignedIn.workout;
            }
            else
            {
                MessageBox.Show("Please select workout", "Message");
            }
        }

        //Hanterar info knappen
        private void Infobtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Welcome! In this app you can add your own workouts! Enjoy :)", "Message");
        }

    }
}
