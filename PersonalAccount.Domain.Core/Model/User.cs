using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PersonalAccount.Domain.Core.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int CountVisit { get; set; }

        public string Discription { get; set; }
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Не указан электронный адрес", AllowEmptyStrings = true)]
        public string ImageSrc { get; set; }
        public User()
        {

        }

        public User(int id, string name, string middelName, string lastName, int coutVisit, string image, string discription)
        {
            this.UserId = id;
            this.Name = name;
            this.LastName = lastName;
            this.CountVisit = coutVisit;
            this.ImageName = image;
            this.Discription = discription;
        }
    }
}
