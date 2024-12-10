using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FitTrack2.Windows;

namespace FitTrack2.Klasser
{
    public class User : Person
    {   
        //Egenskaper
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        //Lista för träningspass som är registerade av användare
        public List<Workout> workout { get; set; }

        //Kolla om användaren är inloggad
        public bool SignedIn { get; set; }

        //Konstruktor
        public User(string Username, string Password, string Country, string SecurityQuestion, string SecurityAnswer) : base(Username, Password) 
        {
            this.Country = Country;
            this.SecurityQuestion = SecurityQuestion;
            this.SecurityAnswer = SecurityAnswer;
            this.workout = new List<Workout>();
        }

        //Metod
        public override void SignIn()
        {
            MessageBox.Show("Succesful sign in");
        }


    
      
    }
}
