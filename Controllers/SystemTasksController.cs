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
    public class SystemTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SystemTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SystemTasks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SystemTasks
                .Include(s => s.Parent)
                .Include(s=>s.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SystemTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemTask = await _context.SystemTasks
                .Include(s => s.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemTask == null)
            {
                return NotFound();
            }

            return View(systemTask);
        }

        // GET: SystemTasks/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.SystemTasks, "Id", "Name");
            return View();
        }

        // POST: SystemTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SystemTask systemTask)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            systemTask.CreatedOn = DateTime.Now;
            systemTask.CreatedById = userId;

            _context.Add(systemTask);
            await _context.SaveChangesAsync(userId);
            return RedirectToAction(nameof(Index));

            ViewData["ParentId"] = new SelectList(_context.SystemTasks, "Id", "Name", systemTask.ParentId);
            return View(systemTask);
        }

        // GET: SystemTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemTask = await _context.SystemTasks.FindAsync(id);
            if (systemTask == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.SystemTasks, "Id", "Id", systemTask.ParentId);
            return View(systemTask);
        }

        // POST: SystemTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SystemTask systemTask)
        {
            if (id != systemTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    systemTask.ModifiedOn = DateTime.Now;
                    systemTask.ModifiedById = userId;

                    _context.Update(systemTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemTaskExists(systemTask.Id))
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
            ViewData["ParentId"] = new SelectList(_context.SystemTasks, "Id", "Id", systemTask.ParentId);
            return View(systemTask);
        }

        // GET: SystemTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemTask = await _context.SystemTasks
                .Include(s => s.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemTask == null)
            {
                return NotFound();
            }

            return View(systemTask);
        }

        // POST: SystemTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var systemTask = await _context.SystemTasks.FindAsync(id);
            if (systemTask != null)
            {
                _context.SystemTasks.Remove(systemTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemTaskExists(int id)
        {
            return _context.SystemTasks.Any(e => e.Id == id);
        }
    }
}
