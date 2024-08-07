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
    public class UserRoleProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserRoleProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserRoleProfiles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context
                .UserRoleProfiles
                .Include(u => u.CreatedBy)
                .Include(u => u.ModifiedBy)
                .Include(u => u.Role)
                .Include(u => u.Task);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserRoleProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoleProfile = await _context.UserRoleProfiles
                .Include(u => u.CreatedBy)
                .Include(u => u.ModifiedBy)
                .Include(u => u.Role)
                .Include(u => u.Task)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRoleProfile == null)
            {
                return NotFound();
            }

            return View(userRoleProfile);
        }

        // GET: UserRoleProfiles/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            ViewData["TaskId"] = new SelectList(_context.SystemTasks, "Id", "Name");
            return View();
        }

        // POST: UserRoleProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRoleProfile userRoleProfile)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userRoleProfile.CreatedOn = DateTime.Now;
            userRoleProfile.CreatedById = userId;
            _context.Add(userRoleProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userRoleProfile.RoleId);
            ViewData["TaskId"] = new SelectList(_context.SystemTasks, "Id", "Name", userRoleProfile.TaskId);
            return View(userRoleProfile);
        }

        // GET: UserRoleProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoleProfile = await _context.UserRoleProfiles.FindAsync(id);
            if (userRoleProfile == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userRoleProfile.RoleId);
            ViewData["TaskId"] = new SelectList(_context.SystemTasks, "Id", "Name", userRoleProfile.TaskId);
            return View(userRoleProfile);
        }

        // POST: UserRoleProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserRoleProfile userRoleProfile)
        {
            if (id != userRoleProfile.Id)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userRoleProfile.CreatedOn = DateTime.Now;
            userRoleProfile.CreatedById = userId;
            try
            {
                _context.Update(userRoleProfile);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRoleProfileExists(userRoleProfile.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userRoleProfile.RoleId);
            ViewData["TaskId"] = new SelectList(_context.SystemTasks, "Id", "Name", userRoleProfile.TaskId);
            return View(userRoleProfile);
        }

        // GET: UserRoleProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoleProfile = await _context.UserRoleProfiles
                .Include(u => u.CreatedBy)
                .Include(u => u.ModifiedBy)
                .Include(u => u.Role)
                .Include(u => u.Task)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRoleProfile == null)
            {
                return NotFound();
            }

            return View(userRoleProfile);
        }

        // POST: UserRoleProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRoleProfile = await _context.UserRoleProfiles.FindAsync(id);
            if (userRoleProfile != null)
            {
                _context.UserRoleProfiles.Remove(userRoleProfile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRoleProfileExists(int id)
        {
            return _context.UserRoleProfiles.Any(e => e.Id == id);
        }
    }
}
