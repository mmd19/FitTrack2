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
        public WorkoutsWindow(User UserSignedIn)
        {
            InitializeComponent();
            DataContext = this;
            this.UserSignedIn = UserSignedIn;
            UsernametxtBlock.Text = UserSignedIn.Username;
            Workouts();
        }


        public void Workouts()
        {
            WorkoutList.ItemsSource = UserSignedIn.workout;
        }

        public void UpdateWorkoutList()
        {
            WorkoutList.ItemsSource = null;
            WorkoutList.ItemsSource = UserSignedIn.workout;
        }

        private void Userbtn_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow(UserSignedIn);
            userDetailsWindow.Show();
            this.Close();
        }

        private void SignOutbtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }

        private void AddWorkoutbtn_Click(object sender, RoutedEventArgs e)
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow(UserSignedIn.workout);
            addWorkoutWindow.Closed += (s, args) => UpdateWorkoutList(); // Uppdatera listan när fönstret stängs
            addWorkoutWindow.Show();

        }

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
                MessageBox.Show("Please select workout", "Message");
            }

        }

        private void RemoveWorkoutbtn_Click(object sender, RoutedEventArgs e)
        {

            if (WorkoutList.SelectedItem is Workout selectedWorkout)
            {
                if (UserSignedIn is AdminUser)
                {
                    User usertest = null;
                    foreach (User user in UserManager.Instance.RegisteredUsers)
                    {
                        foreach (Workout workout in user.workout)
                        {
                            if (selectedWorkout.Equals(workout))
                            {
                                usertest = user;
                            }
                        }
                    }
                    if (usertest != null)
                    {
                        usertest.workout.Remove(selectedWorkout);
                        UserSignedIn.workout.Remove(selectedWorkout);
                    }
                } else
                {
                    UserSignedIn.workout.Remove(selectedWorkout);

                }
                WorkoutList.ItemsSource = null;
                WorkoutList.ItemsSource = UserSignedIn.workout;
            }
            else
            {
                MessageBox.Show("Please select workout", "Message");
            }
        }

        private void Infobtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Welcome! In this app you can add your own workouts! Enjoy :)", "Message");
        }

    }
}
