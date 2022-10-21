using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CRUD_USERS.Models
{
    public class UserModel
    {
        private int id;
        private string name;
        private string lastName;
        private string address;
        private string city;
        private string state;

        [DisplayName("User ID")]
        public int Id { get => id; set => id = value; }
        [DisplayName("Nome")]
        [Required(ErrorMessage = "User Name is required")]
        [StringLength(50, ErrorMessage = "User name is very long!")]
        public string Name { get => name; set => name = value; }

        [DisplayName("Sobrenome")]
        [Required(ErrorMessage = "User Name is required")]
        [StringLength(50, ErrorMessage = "User last name is very long!")]
        public string LastName { get => lastName; set => lastName = value; }

        [DisplayName("Endereço")]
        [Required(ErrorMessage = "User Name is required")]
        public string Address { get => address; set => address = value; }

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "User City is required")]
        public string City { get => city; set => city = value; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "User Name is required")]
        [StringLength(2, ErrorMessage = "User State is very long!")]
        public string State { get => state; set => state = value; }
    }
}
