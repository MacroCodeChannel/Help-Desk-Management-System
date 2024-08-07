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
    public class SystemSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SystemSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SystemSettings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SystemSettings.Include(s => s.CreatedBy).Include(s => s.ModifiedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SystemSettings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemSetting = await _context.SystemSettings
                .Include(s => s.CreatedBy)
                .Include(s => s.ModifiedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemSetting == null)
            {
                return NotFound();
            }

            return View(systemSetting);
        }

        // GET: SystemSettings/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ModifiedById"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: SystemSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SystemSetting systemSetting)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            systemSetting.CreatedOn = DateTime.Now;
            systemSetting.CreatedById = userId;

            _context.Add(systemSetting);
            await _context.SaveChangesAsync(userId);
            return RedirectToAction(nameof(Index));


            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", systemSetting.CreatedById);
            ViewData["ModifiedById"] = new SelectList(_context.Users, "Id", "Id", systemSetting.ModifiedById);
            return View(systemSetting);
        }

        // GET: SystemSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemSetting = await _context.SystemSettings.FindAsync(id);
            if (systemSetting == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", systemSetting.CreatedById);
            ViewData["ModifiedById"] = new SelectList(_context.Users, "Id", "Id", systemSetting.ModifiedById);
            return View(systemSetting);
        }

        // POST: SystemSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,Value,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] SystemSetting systemSetting)
        {
            if (id != systemSetting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemSettingExists(systemSetting.Id))
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
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", systemSetting.CreatedById);
            ViewData["ModifiedById"] = new SelectList(_context.Users, "Id", "Id", systemSetting.ModifiedById);
            return View(systemSetting);
        }

        // GET: SystemSettings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemSetting = await _context.SystemSettings
                .Include(s => s.CreatedBy)
                .Include(s => s.ModifiedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemSetting == null)
            {
                return NotFound();
            }

            return View(systemSetting);
        }

        // POST: SystemSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var systemSetting = await _context.SystemSettings.FindAsync(id);
            if (systemSetting != null)
            {
                _context.SystemSettings.Remove(systemSetting);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemSettingExists(int id)
        {
            return _context.SystemSettings.Any(e => e.Id == id);
        }
    }
}
