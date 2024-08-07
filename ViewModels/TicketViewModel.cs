using HelpDeskSystem.Models;
using System.ComponentModel;

namespace HelpDeskSystem.ViewModels
{
    public class TicketViewModel
    {
        [DisplayName("No")]
        public int Id { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Description")]

        public string Description { get; set; }


        [DisplayName("Status")]
        public int StatusId { get; set; }

        public SystemCodeDetail Status { get; set; }

        [DisplayName("Priority")]
        public int PriorityId { get; set; }
        public SystemCodeDetail Priority { get; set; }

        [DisplayName("Created By")]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }


        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Ticket Category")]
        public int CategoryId { get; set; }

        [DisplayName("Ticket Sub-Category")]
        public int SubCategoryId { get; set; }
        public TicketSubCategory SubCategory { get; set; }

        public List<Ticket> Tickets { get; set; }

        public Ticket TicketDetails { get; set; }

        public List<Comment> TicketComments { get; set; }

        public List<TicketResolution> TicketResolutions { get; set; }
        public Comment TicketComment { get; set; }

        public TicketResolution Resolution { get; set; }

        [DisplayName("Attachment")]
        public string Attachment { get; set; }


        [DisplayName("Comment Description")]
        public string CommentDescription { get; set; }

        [DisplayName("Assigned To")]
        public string? AssignedToId { get; set; }
        public ApplicationUser AssignedTo { get; set; }

        [DisplayName("Assigned On")]
        public DateTime? AssignedOn { get; set; }


        [DisplayName("Ticket Category")]
        public int TicketCategoryId { get; set; }

        public SystemSetting MainDuration { get; set; }
    }

}
