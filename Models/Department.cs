using Microsoft.CodeAnalysis.Diagnostics;
using System.ComponentModel;

namespace HelpDeskSystem.Models
{
    public class Department :UserActivity
    {

        [DisplayName("No")]
         public int Id { get; set; }


        [DisplayName("Department Code")]
        public string Code { get; set; }


        [DisplayName("Department Name")]
        public string Name { get; set; }
    }
}
