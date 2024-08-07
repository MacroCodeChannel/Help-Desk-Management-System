using System.ComponentModel;

namespace HelpDeskSystem.Models
{
    public class Country :UserActivity
    {       
        public int Id { get; set; }


        [DisplayName("Country Code")]
        public string Code { get; set; }

        [DisplayName("Country Name")]
        public string Name { get; set; }
    }
}
