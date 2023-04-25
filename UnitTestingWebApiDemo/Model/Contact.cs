using System.ComponentModel.DataAnnotations;

namespace UnitTestingWebApiDemo.Model
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }

    }
}
