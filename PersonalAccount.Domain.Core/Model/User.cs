using System.ComponentModel.DataAnnotations;

namespace PersonalAccount.Domain.Core.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string MiddelName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int CountVisit { get; set; }

        public User()
        {

        }

        public User(int id, string name, string middelName, string lastName, int coutVisit)
        {
            this.UserId = id;
            this.Name = name;
            this.MiddelName = middelName;
            this.LastName = lastName;
            this.CountVisit = coutVisit;
        }
    }
}
