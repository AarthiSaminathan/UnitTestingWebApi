using System.ComponentModel.DataAnnotations;

namespace UnitTestingWebApiDemo.Model
{
    public class AddContact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
