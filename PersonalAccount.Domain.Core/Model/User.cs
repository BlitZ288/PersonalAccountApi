namespace PersonalAccount.Domain.Core.Model
{
    public class User
    {
        private int id;
        public string name;
        private string middelName;
        private string lastName;
        public string password;
        private int coutVisit;

        public User()
        {

        }

        public User(int id, string name, string middelName, string lastName, int coutVisit)
        {
            this.id = id;
            this.name = name;
            this.middelName = middelName;
            this.lastName = lastName;
            this.coutVisit = coutVisit;
        }
    }
}
