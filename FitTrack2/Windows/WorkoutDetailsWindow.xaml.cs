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
    /// Interaction logic for WorkoutDetailsWindow.xaml
    /// </summary>
    public partial class WorkoutDetailsWindow : Window
    {
        public User UserSignedIn { get; set; }
        public Workout selectedWorkout { get; set; }

        public WorkoutDetailsWindow(Workout selectedWorkout, User UserSignedIn)
        {
            InitializeComponent();
            DataContext = this;
            this.selectedWorkout = selectedWorkout;
            this.UserSignedIn = UserSignedIn;
            Type();
            DisplayDetails();

            Savebtn.IsEnabled = false;

        }

        public void Type()
        {
            List<string> type = new List<string>
            {
                "Cardio", "Strength"
            };
            TypeComboBox.ItemsSource = type;
        }


        private void DisplayDetails()
        {
            DatetxtBox.Text = selectedWorkout.Date.ToString("yyyy-MM-dd");
            TypeComboBox.Text = selectedWorkout.Type;
            DurationtxtBox.Text = selectedWorkout.Duration.ToString();
            CaloriesBurnedtxtBox.Text = selectedWorkout.CaloriesBurned.ToString();
            DistancetxtBox.Text = (selectedWorkout is CardioWorkout cardioWorkout) ? cardioWorkout.Distance.ToString() : "";
            RepetitionstxtBox.Text = (selectedWorkout is StrengthWorkout strengthWorkout) ? strengthWorkout.Repetitions.ToString() : "";
            NotestxtBox.Text = selectedWorkout.Notes;

        }

       

        private void Editbtn_Click(object sender, RoutedEventArgs e)
        {
            
          
            DatetxtBox.IsReadOnly = false;
            TypeComboBox.IsEnabled = false;
            DurationtxtBox.IsReadOnly = false;
            CaloriesBurnedtxtBox.IsReadOnly=false;
            NotestxtBox.IsReadOnly = false ;

            string selectedType = TypeComboBox.Text;
            

            if (selectedType == "Cardio")
            {
                RepetitionstxtBox.IsReadOnly = true;
                DistancetxtBox.IsReadOnly = false;
            }
            else if (selectedType == "Strength")
            {
                RepetitionstxtBox.IsReadOnly = false;
                DistancetxtBox.IsReadOnly = true;
            }
            else
            {
                MessageBox.Show("Select type", "Message");
            }

            Savebtn.IsEnabled = true;

        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            if (string.IsNullOrEmpty(DatetxtBox.Text) ||
                string.IsNullOrEmpty(TypeComboBox.Text) ||
                string.IsNullOrEmpty(DurationtxtBox.Text) ||
                string.IsNullOrEmpty(NotestxtBox.Text))
            {
                MessageBox.Show("Please,fill in all field");
            }
            DateTime date = DateTime.Parse(DatetxtBox.Text);
            string type = TypeComboBox.Text;
            TimeSpan duration = TimeSpan.Parse(DurationtxtBox.Text);
            int caloriesBurned = int.Parse(CaloriesBurnedtxtBox.Text);
            string.IsNullOrEmpty(CaloriesBurnedtxtBox.Text);
            string notes = NotestxtBox.Text;

            if (type == "Cardio")
            {

                int distance = int.Parse(DistancetxtBox.Text);
                    selectedWorkout.Date = date;
                    selectedWorkout.Type = type;
                    selectedWorkout.Duration = duration;
                    selectedWorkout.CaloriesBurned = caloriesBurned;
                    selectedWorkout.Notes = notes;
                    ((CardioWorkout) selectedWorkout).Distance = distance;
                
                MessageBox.Show("Workout changed", "Message");
                    WorkoutsWindow workoutsWindow = new WorkoutsWindow(UserSignedIn);
                    workoutsWindow.Show();
                    this.Close();
            }
            else if (type == "Strength")
            {
                int repetitions = int.Parse(RepetitionstxtBox.Text);
                    selectedWorkout.Date = date;
                    selectedWorkout.Type = type;
                    selectedWorkout.Duration = duration;
                    selectedWorkout.CaloriesBurned = caloriesBurned;
                    selectedWorkout.Notes = notes;
                    ((StrengthWorkout) selectedWorkout).Repetitions = repetitions;
                MessageBox.Show("Workout changed", "Message");
                    WorkoutsWindow workoutsWindow = new WorkoutsWindow(UserSignedIn);
                    workoutsWindow.Show();
                    this.Close();
            }
            } catch {
                MessageBox.Show("Please,fill in all field valid input", "Message");

            }
         
        }
        
    }
    
}
