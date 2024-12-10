using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitTrack2.Windows
{
    /// <summary>
    /// Interaction logic for AddWorkoutWindow.xaml
    /// </summary>
    public partial class AddWorkoutWindow : Window
    {

        //Lista för lagring av träningspass
        public List<Workout> workouts { get; set; }
        
        //Konstruktor
        public AddWorkoutWindow(List<Workout> workouts)
        {
            InitializeComponent();
            this.workouts = workouts;
            Type();
        }

        //Metod för att kunna fylla i Combobox med två alternativ
        public void Type()
        {
            List<string> type = new List<string>
            {
                "Cardio", "Strength"
            };
            TypeComboBox.ItemsSource = type;
        }

        //Hanterar knapp tryck
        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Hämtar och omvandlar inmatad data
                DateTime date = DateTime.Parse(DatetxtBox.Text);
                string type = TypeComboBox.Text;
                TimeSpan duration = TimeSpan.Parse(DurationtxtBox.Text);
                int caloriesBurned = int.Parse(CaloriesBurnedtxtBox.Text);
                string notes = NotestxtBox.Text;

                //Säkerställer att alla fält är ifyllda
                if (string.IsNullOrEmpty(DatetxtBox.Text) ||
                    string.IsNullOrEmpty(TypeComboBox.Text) ||
                    string.IsNullOrEmpty(DurationtxtBox.Text) ||
                    string.IsNullOrEmpty(CaloriesBurnedtxtBox.Text) ||
                    string.IsNullOrEmpty(NotestxtBox.Text))
                {
                    MessageBox.Show("Please, fill in all fields", "Message");
                }

                //Lägger till träningspass beroende på typ
                else if (type == "Cardio")
                {
                    int distance = int.Parse(DistancetxtBox.Text);
                    workouts.Add(new CardioWorkout(date, type, duration, caloriesBurned, notes, distance));
                    MessageBox.Show("Workout added", "Message");
                    this.Close();
                }
                else if (type == "Strength")
                {
                    int repetitions = int.Parse(RepetitionstxtBox.Text);
                    workouts.Add(new StrengthWorkout(date, type, duration, caloriesBurned, notes, repetitions));
                    MessageBox.Show("Workout added", "Message");
                    this.Close();
                }
            }

            //Fångar upp fel
            catch
            {
                MessageBox.Show("Invalid input", "Message");
            }
        }

        
    }
}
