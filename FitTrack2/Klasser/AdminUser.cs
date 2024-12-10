using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace FitTrack2.Klasser
    {
        // Ärver från User
        public class AdminUser :User
    {
        //Konstruktor
        public AdminUser(string Username, string Password, string Country, string SecurityQuestion, string SecurityAnswer) : base(Username, Password, Country, SecurityQuestion, SecurityAnswer)
        { 
        }
        
        //Metod
        public void ManageAllWorkouts()
        {
            List<Workout> workoutList = new List<Workout> { };
            foreach (User user in UserManager.Instance.RegisteredUsers)
            {
                if (user is not AdminUser)
                {
                    foreach (Workout workout in user.workout)
                    {
                        workoutList.Add(workout);
                    }
                }
            }
            this.workout = workoutList;
        }
    }
}
