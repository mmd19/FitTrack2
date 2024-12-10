using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace FitTrack2.Klasser
{
    public class UserManager
    {
        //Samling för registrerade användare
        public ObservableCollection<User> RegisteredUsers {  get; set; }

        //Skapar instans
        private static UserManager instance;

        //Egenskap som sätter singleton instans av UserManager
        public static UserManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserManager();
                }
                return instance;
            }
            set { instance = value; }
        }

        // Privat konstruktor
        private UserManager()
        {
            User user = new User("user", "password", "Sweden", "What is the name of your childhood best friend?", "Anna");
            AdminUser admin = new AdminUser("admin", "password", "Sweden", "What is your favorite color?", "blue");
            RegisteredUsers = new ObservableCollection<User>();
            RegisteredUsers.Add(admin); // För admin
            RegisteredUsers.Add(user); // För user

            // För user ska ha 2 olika träningspass
            user.workout.Add(new CardioWorkout(DateTime.Today.AddDays(4), "Cardio", TimeSpan.FromSeconds(1), 15, "Bra", 15));
            user.workout.Add(new StrengthWorkout(DateTime.Now, "Strength", TimeSpan.FromSeconds(4), 120, "strength", 10));
        }

        //Hämtar användare med hjälp av användarnamn
        public User GetUsers(string username)
        {
            return RegisteredUsers.Where(u => u.Username == username).FirstOrDefault();
        }


        // Kollar om användare med användarnamnet redan finns
        public bool UserExist(string username)
        {
            return RegisteredUsers.Any(u => u.Username == username);
        }
        
        //Lägger till ny användare om användarnamnet inte redan finns
        public bool AddUser(User newUser)
        {
            if (UserExist(newUser.Username))
            {
                return false;
            }
            RegisteredUsers.Add(newUser);
            return true;
        }

    
    
    }
}
