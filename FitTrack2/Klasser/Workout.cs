using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace FitTrack2.Klasser
{
    public abstract class Workout
    {
        //Egenskaper
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }

        //Konstruktor
        public Workout(DateTime Date, string Type, TimeSpan Duration, int CaloriesBurned, string Notes) 
        {
            this.Date = Date;
            this.Type = Type;
            this.Duration = Duration;
            this.CaloriesBurned = CaloriesBurned;
            this.Notes = Notes;
        }

        //Metod
        public abstract int CalculateCaloriesBurned();
    }
}
