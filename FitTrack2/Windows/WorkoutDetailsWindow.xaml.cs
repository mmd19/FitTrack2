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
        //Egenskaper
        public User UserSignedIn { get; set; }
        public Workout selectedWorkout { get; set; }

        //Konstruktor
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

        //Metod för att kunna fylla i typ av träning
        public void Type()
        {
            List<string> type = new List<string>
            {
                "Cardio", "Strength"
            };
            TypeComboBox.ItemsSource = type;
        }

        //Metod för att kunna visa detaljer för de valda träningspasset
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

        //Hanterar redigera knappen
        private void Editbtn_Click(object sender, RoutedEventArgs e)
        {   
            //Låser upp fält för att kunna redigera
            DatetxtBox.IsReadOnly = false;
            TypeComboBox.IsEnabled = false; //Går inte att ändra
            DurationtxtBox.IsReadOnly = false;
            CaloriesBurnedtxtBox.IsReadOnly=false;
            NotestxtBox.IsReadOnly = false ;

            string selectedType = TypeComboBox.Text; //Kollar vald typ
            
            //Om typ är cardio
            if (selectedType == "Cardio")
            {
                RepetitionstxtBox.IsReadOnly = true; //Går inte att ändra
                DistancetxtBox.IsReadOnly = false; // Går att ändra
            }

            //Om typ är strength
            else if (selectedType == "Strength")
            {
                RepetitionstxtBox.IsReadOnly = false; //Går att ändra
                DistancetxtBox.IsReadOnly = true; //Går inte att ändra
            }


            Savebtn.IsEnabled = true; //Aktiverar spara knappen

        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Kollar om alla obligtoriska fält är ifyllda
            if (string.IsNullOrEmpty(DatetxtBox.Text) ||
                string.IsNullOrEmpty(TypeComboBox.Text) ||
                string.IsNullOrEmpty(DurationtxtBox.Text) ||
                string.IsNullOrEmpty(NotestxtBox.Text))
            {
                MessageBox.Show("Please,fill in all field");
            }

            //För att kunna uppdatera träningspasset
            DateTime date = DateTime.Parse(DatetxtBox.Text);
            string type = TypeComboBox.Text;
            TimeSpan duration = TimeSpan.Parse(DurationtxtBox.Text);
            int caloriesBurned = int.Parse(CaloriesBurnedtxtBox.Text);
            string.IsNullOrEmpty(CaloriesBurnedtxtBox.Text);
            string notes = NotestxtBox.Text;

            if (type == "Cardio")
            {
                    //För cardio
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
                    //För strength
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

                //Hanterar fel vid felaktig inmatning
                MessageBox.Show("Please,fill in all field valid input", "Message");

            }
         
        }
        
    }
    
}
