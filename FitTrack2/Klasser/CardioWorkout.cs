using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack2.Klasser
{
    public class CardioWorkout : Workout
    {
        //Egenskap
        public int Distance { get; set; }

        // Konstruktor
        public CardioWorkout(DateTime Date, string Type, TimeSpan Duration, int CaloriesBurned, string Notes, int Distance) : base(Date, Type, Duration, CaloriesBurned, Notes)
        {
            this.Distance = Distance;
        }

        //Metod
        public override int CalculateCaloriesBurned()
        { 
            return Distance * 15;
        }
    }
}
