using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack2.Klasser
{
    public class StrengthWorkout : Workout
    {
        //Egenskap
        public int Repetitions { get; set; }

        //Konstruktor
        public StrengthWorkout(DateTime Date, string Type, TimeSpan Duration, int CaloriesBurned, string Notes, int Repetitions) : base(Date, Type, Duration, CaloriesBurned, Notes)
        {
            this.Repetitions = Repetitions;
        }

        //Metod
        public override int CalculateCaloriesBurned()
        {
            return (int)(Repetitions * 10);
        }
    }
}
