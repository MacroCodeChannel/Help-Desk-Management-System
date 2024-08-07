using HelpDeskSystem.Models;

namespace HelpDeskSystem.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public DateTime CreatedOn { get; set; }

        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public Comment Comment { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
