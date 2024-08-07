using HelpDeskSystem.Models;
using System.ComponentModel;

namespace HelpDeskSystem.ViewModels
{
    public class ProfileViewModel
    {

        public ICollection<UserRoleProfile> Profiles { get; set; }


        public ICollection<int> RightsIdsAssigned { get; set; }

        [DisplayName("Role Name")]
        public string RoleId { get;set; }


        [DisplayName("System Task")]
        public int TaskId { get; set; }
    }
}
