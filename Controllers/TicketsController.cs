using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpDeskSystem.Data;
using HelpDeskSystem.Models;
using System.Security.Claims;
using HelpDeskSystem.ViewModels;
using System.Runtime.Intrinsics.X86;

namespace HelpDeskSystem.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
         private readonly  IConfiguration _configuration;

        public TicketsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET:Tickets
        public async Task<IActionResult> Index(TicketViewModel vm)
        {
            vm.Tickets =  await _context.Tickets
                .Include(t => t.CreatedBy)
                .Include(t=> t.SubCategory)
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t=>t.TicketComments)
                .OrderBy(x=>x.CreatedOn)
                .ToListAsync();

            return View(vm);
        }

        public async Task<IActionResult> AssignedTickets(TicketViewModel vm)
        {
            var assignedStatus = await _context
            .SystemCodeDetails
            .Include(x => x.SystemCode)
            .Where(x => x.SystemCode.Code == "ResolutionStatus" && x.Code == "Assigned")
            .FirstOrDefaultAsync();

            vm.Tickets = await _context.Tickets
                .Include(t => t.CreatedBy)
                .Include(t => t.SubCategory)
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.TicketComments)
                .OrderBy(x => x.CreatedOn)
                .Where(x=>x.StatusId== assignedStatus.Id)
                .ToListAsync();

            return View(vm);
        }

        public async Task<IActionResult> ClosedTickets(TicketViewModel vm)
        {
            var closedStatus = await _context
            .SystemCodeDetails
            .Include(x => x.SystemCode)
            .Where(x => x.SystemCode.Code == "ResolutionStatus" && x.Code == "Closed")
            .FirstOrDefaultAsync();

            vm.Tickets = await _context.Tickets
                .Include(t => t.CreatedBy)
                .Include(t => t.SubCategory)
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.TicketComments)
                .OrderBy(x => x.CreatedOn)
                .Where(x => x.StatusId == closedStatus.Id)
                .ToListAsync();

            return View(vm);
        }

        public async Task<IActionResult> ResolvedTickets(TicketViewModel vm)
        {
            var resolvedStatus = await _context
            .SystemCodeDetails
            .Include(x => x.SystemCode)
            .Where(x => x.SystemCode.Code == "ResolutionStatus" && x.Code == "Resolved")
            .FirstOrDefaultAsync();

            vm.Tickets = await _context.Tickets
                .Include(t => t.CreatedBy)
                .Include(t => t.SubCategory)
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.TicketComments)
                .OrderBy(x => x.CreatedOn)
                .Where(x => x.StatusId == resolvedStatus.Id)
                .ToListAsync();

            return View(vm);
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id, TicketViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

             vm.TicketDetails = await _context.Tickets
                .Include(t => t.CreatedBy)
                .Include (t=> t.SubCategory)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.AssignedTo)
                .FirstOrDefaultAsync(m => m.Id == id);

            vm.TicketComments = await _context.Comments
                .Include(t => t.CreatedBy)
                .Include(t => t.Ticket)
                .Where(t=>t.TicketId==id)
                .ToListAsync();

            vm.TicketResolutions = await _context.TicketResolutions
                   .Include(t => t.CreatedBy)
                   .Include(t => t.Ticket)
                   .Include(t => t.Status)
                   .Where(t => t.TicketId == id)
                   .ToListAsync();

            if (vm.TicketDetails == null)
            {
                return NotFound();
            }

            return View(vm);
        }
        public async Task<IActionResult> ReOpen(int? id, TicketViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.TicketDetails = await _context.Tickets
               .Include(t => t.CreatedBy)
               .Include(t => t.SubCategory)
               .Include(t => t.Status)
               .Include(t => t.Priority)
               .Include(t => t.AssignedTo)
               .FirstOrDefaultAsync(m => m.Id == id);

            vm.TicketComments = await _context.Comments
                .Include(t => t.CreatedBy)
                .Include(t => t.Ticket)
                .Where(t => t.TicketId == id)
                .ToListAsync();

            vm.TicketResolutions = await _context.TicketResolutions
               .Include(t => t.CreatedBy)
               .Include(t => t.Ticket)
               .Include(t => t.Status)
               .Where(t => t.TicketId == id)
               .ToListAsync();


            if (vm.TicketDetails == null)
            {
                return NotFound();
            }


            ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "ResolutionStatus"), "Id", "Description");

            return View(vm);
        }

        public async Task<IActionResult> TicketAssignment(int? id, TicketViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.TicketDetails = await _context.Tickets
               .Include(t => t.CreatedBy)
               .Include(t => t.SubCategory)
               .Include(t => t.Status)
               .Include(t => t.Priority)
               .Include(t => t.AssignedTo)
               .FirstOrDefaultAsync(m => m.Id == id);

            vm.TicketComments = await _context.Comments
                .Include(t => t.CreatedBy)
                .Include(t => t.Ticket)
                .Where(t => t.TicketId == id)
                .ToListAsync();

            vm.TicketResolutions = await _context.TicketResolutions
               .Include(t => t.CreatedBy)
               .Include(t => t.Ticket)
               .Include(t => t.Status)
               .Where(t => t.TicketId == id)
               .ToListAsync();


            if (vm.TicketDetails == null)
            {
                return NotFound();
            }


            ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "ResolutionStatus"), "Id", "Description");

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");

            return View(vm);
        }

        public async Task<IActionResult> Resolve(int? id, TicketViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.TicketDetails = await _context.Tickets
               .Include(t => t.CreatedBy)
               .Include(t => t.SubCategory)
               .Include(t => t.Status)
               .Include(t => t.Priority)
                .Include(t => t.AssignedTo)
               .FirstOrDefaultAsync(m => m.Id == id);

            vm.TicketComments = await _context.Comments
                .Include(t => t.CreatedBy)
                .Include(t => t.Ticket)
                .Where(t => t.TicketId == id)
                .ToListAsync();

            vm.TicketResolutions = await _context.TicketResolutions
               .Include(t => t.CreatedBy)
               .Include(t => t.Ticket)
               .Include(t => t.Status)
               .Where(t => t.TicketId == id)
               .ToListAsync();


            if (vm.TicketDetails == null)
            {
                return NotFound();
            }


            ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "ResolutionStatus"), "Id", "Description");

            return View(vm);
        }
        // GET: Tickets/Create
        public IActionResult Create()
        {

            ViewData["PriorityId"] = new SelectList(_context.SystemCodeDetails.Include(x=>x.SystemCode).Where(x=>x.SystemCode.Code== "Priority"), "Id", "Description");
            ViewData["CategoryId"] = new SelectList(_context.TicketCategories, "Id", "Name");
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "FullName");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketViewModel ticketvm, IFormFile attachment)
        {

            if(attachment !=null  && attachment.Length > 0)
            {
                 var filename = "Ticket_Attachment"+ DateTime.Now.ToString("yyyymmddhhmmss")+"_"+ attachment.FileName;
                var path = _configuration["FileSettings:UploadsFolder"]!;
                var filepath = Path.Combine(path, filename);
                var stream = new FileStream(filepath,FileMode.Create);
                await attachment.CopyToAsync(stream);
                ticketvm.Attachment = filename;
            }


            var pendingstatus = await _context
                .SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.SystemCode.Code == "Status" && x.Code == "Pending")
                .FirstOrDefaultAsync();
                
            Ticket ticket = new();
            ticket.Id = ticketvm.Id;
            ticket.Title = ticketvm.Title;
            ticket.Description = ticketvm.Description;
            ticket.StatusId = pendingstatus.Id;
            ticket.PriorityId = ticketvm.PriorityId;
            ticket.SubCategoryId = ticketvm.SubCategoryId;
            ticket.Attachment = ticketvm.Attachment;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ticket.CreatedOn = DateTime.Now;
            ticket.CreatedById = userId;
            _context.Add(ticket);
            await _context.SaveChangesAsync();


            //Log the Audit Trail
            var activity = new AuditTrail
            {
                Action = "Create",
                TimeStamp = DateTime.Now,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "Tickets",
                AffectedTable = "Tickets"
            };

            _context.Add(activity);
            await _context.SaveChangesAsync();

            TempData["MESSAGE"] = "Ticket Details successfully Created";


            ViewData["PriorityId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "Priority"), "Id", "Description");

            ViewData["CategoryId"] = new SelectList(_context.TicketCategories, "Id", "Name");

            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "FullName", ticket.CreatedById);

            return RedirectToAction(nameof(Index));
            
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int id,TicketViewModel vm)
        {
            //Logged In User
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Comment newcomment = new();
            newcomment.TicketId = id;
            newcomment.CreatedOn = DateTime.Now;
            newcomment.CreatedById = userId;
            newcomment.Description = vm.CommentDescription;
            _context.Add(newcomment);
            await _context.SaveChangesAsync();

            //Log the Audit Trail
            var activity = new AuditTrail
            {
                Action = "Create",
                TimeStamp = DateTime.Now,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "Comments",
                AffectedTable = "Comments"
            };

            _context.Add(activity);
            await _context.SaveChangesAsync();

            TempData["MESSAGE"] = "Comments Details successfully Created";

            return RedirectToAction("Details",new { id=id});
        }

       [HttpPost]
        public async Task<IActionResult> AssignedConfirmed(int id, TicketViewModel vm)
        {

            var reassignedstatus = await _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.SystemCode.Code == "ResolutionStatus" && x.Code == "Assigned")
                .FirstOrDefaultAsync();

            //Logged In User
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TicketResolution resolution = new();
            resolution.TicketId = id;
            resolution.StatusId = reassignedstatus.Id;
            resolution.CreatedOn = DateTime.Now;
            resolution.CreatedById = userId;
            resolution.Description = "Ticket Assigned";
            _context.Add(resolution);

            var ticket = await _context.Tickets
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            ticket.StatusId = reassignedstatus.Id;
            ticket.AssignedToId = vm.AssignedToId;
            ticket.AssignedOn = DateTime.Now;
            _context.Update(ticket);

            await _context.SaveChangesAsync();

            //Log the Audit Trail
            var activity = new AuditTrail
            {
                Action = "Re-Open",
                TimeStamp = DateTime.Now,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "TicketResolutions",
                AffectedTable = "TicketResolutions"
            };

            _context.Add(activity);
            await _context.SaveChangesAsync();

            TempData["MESSAGE"] = "Ticket Re-Opened successfully";

            return RedirectToAction("Resolve", new { id = id });
        }



        [HttpPost]
        public async Task<IActionResult> ReOpenConfirmed(int id, TicketViewModel vm)
        {

            var closedstatus = await _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.SystemCode.Code == "ResolutionStatus" && x.Code == "ReOpened")
                .FirstOrDefaultAsync();

            //Logged In User
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TicketResolution resolution = new();
            resolution.TicketId = id;
            resolution.StatusId = closedstatus.Id;
            resolution.CreatedOn = DateTime.Now;
            resolution.CreatedById = userId;
            resolution.Description = "Ticket Re-Opened";
            _context.Add(resolution);

            var ticket = await _context.Tickets
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            ticket.StatusId = closedstatus.Id;
            _context.Update(ticket);

            await _context.SaveChangesAsync();

            //Log the Audit Trail
            var activity = new AuditTrail
            {
                Action = "Re-Open",
                TimeStamp = DateTime.Now,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "TicketResolutions",
                AffectedTable = "TicketResolutions"
            };

            _context.Add(activity);
            await _context.SaveChangesAsync();

            TempData["MESSAGE"] = "Ticket Re-Opened successfully";

            return RedirectToAction("Resolve", new { id = id });
        }


        [HttpPost]
        public async Task<IActionResult> ClosedConfirmed(int id, TicketViewModel vm)
        {

            var closedstatus = await _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.SystemCode.Code == "ResolutionStatus" && x.Code == "Closed")
                .FirstOrDefaultAsync();
             
            //Logged In User
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TicketResolution resolution = new();
            resolution.TicketId = id;
            resolution.StatusId = closedstatus.Id;
            resolution.CreatedOn = DateTime.Now;
            resolution.CreatedById = userId;
            resolution.Description = "Ticket Closed";
            _context.Add(resolution);

            var ticket = await _context.Tickets
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            ticket.StatusId = closedstatus.Id;
            _context.Update(ticket);

            await _context.SaveChangesAsync();

            //Log the Audit Trail
            var activity = new AuditTrail
            {
                Action = "Closed",
                TimeStamp = DateTime.Now,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "TicketResolutions",
                AffectedTable = "TicketResolutions"
            };

            _context.Add(activity);
            await _context.SaveChangesAsync();

            TempData["MESSAGE"] = "Ticket Closed successfully";

            return RedirectToAction("Resolve", new { id = id });
        }


        [HttpPost]
        public async Task<IActionResult> ResolvedConfirmed(int id, TicketViewModel vm)
        {
            //Logged In User
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TicketResolution resolution = new();
            resolution.TicketId = id;
            resolution.StatusId = vm.StatusId;
            resolution.CreatedOn = DateTime.Now;
            resolution.CreatedById = userId;
            resolution.Description = vm.CommentDescription;
            _context.Add(resolution);

            var ticket = await _context.Tickets
                .Where(x=>x.Id ==id)
                .FirstOrDefaultAsync();
            ticket.StatusId = vm.StatusId;
            _context.Update(ticket);

            await _context.SaveChangesAsync();

            //Log the Audit Trail
            var activity = new AuditTrail
            {
                Action = "Create",
                TimeStamp = DateTime.Now,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "TicketResolutions",
                AffectedTable = "TicketResolutions"
            };

            _context.Add(activity);
            await _context.SaveChangesAsync();

            TempData["MESSAGE"] = "Ticket Resolution Details successfully Created";

            return RedirectToAction("Resolve", new { id = id });
        }


        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "FullName", ticket.CreatedById);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Status,Priority,CreatedById,CreatedOn")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                    TempData["MESSAGE"] = "Ticket Details successfully Updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "FullName", ticket.CreatedById);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
