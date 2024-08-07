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

namespace HelpDeskSystem.Controllers
{
    public class TicketCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TicketCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketCategories.Include(t => t.CreatedBy).Include(t => t.ModifiedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TicketCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketCategory = await _context.TicketCategories
                .Include(t => t.CreatedBy)
                .Include(t => t.ModifiedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketCategory == null)
            {
                return NotFound();
            }

            return View(ticketCategory);
        }

        // GET: TicketCategories/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: TicketCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( TicketCategory ticketCategory)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ticketCategory.CreatedOn = DateTime.Now;
            ticketCategory.CreatedById = userId;
            _context.Add(ticketCategory);
            await _context.SaveChangesAsync(userId);

            TempData["MESSAGE"] = "Ticket Category Details successfully Created";
            return RedirectToAction(nameof(Index));
        }

        // GET: TicketCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketCategory = await _context.TicketCategories.FindAsync(id);
            if (ticketCategory == null)
            {
                return NotFound();
            }
              return View(ticketCategory);
        }

        // POST: TicketCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TicketCategory ticketCategory)
        {
            if (id != ticketCategory.Id)
            {
                return NotFound();
            }

                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    ticketCategory.ModifiedOn = DateTime.Now;
                    ticketCategory.ModifiedById = userId;
                     _context.Update(ticketCategory);
                    await _context.SaveChangesAsync();

                TempData["MESSAGE"] = "Ticket Category Details successfully Updated";

            }
            catch (DbUpdateConcurrencyException)
                {
                    if (!TicketCategoryExists(ticketCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
               return View(ticketCategory);
        }

        // GET: TicketCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketCategory = await _context.TicketCategories
                .Include(t => t.CreatedBy)
                .Include(t => t.ModifiedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketCategory == null)
            {
                return NotFound();
            }

            return View(ticketCategory);
        }

        // POST: TicketCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketCategory = await _context.TicketCategories.FindAsync(id);
            if (ticketCategory != null)
            {
                _context.TicketCategories.Remove(ticketCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketCategoryExists(int id)
        {
            return _context.TicketCategories.Any(e => e.Id == id);
        }
    }
}
