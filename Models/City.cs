using System.ComponentModel;

namespace HelpDeskSystem.Models
{
    public class City :UserActivity
    {
        public int Id { get; set; }


        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }


        [DisplayName("City Name")]
        public string Name { get; set; }


        [DisplayName("Country Name")]
        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
