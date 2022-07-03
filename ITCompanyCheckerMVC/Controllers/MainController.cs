using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITCompanyCheckerMVC.Areas.Identity.Data;
using ITCompanyCheckerMVC.Models;

namespace ITCompanyCheckerMVC.Controllers
{
    public class MainController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MainController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Main
        public async Task<IActionResult> Index()
        {
            return _context.Users != null ?
                        View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.EmployeeCRUD'  is null.");
        }

        // GET: Main/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeCRUD == null)
            {
                return NotFound();
            }

            var employeeCRUD = await _context.EmployeeCRUD
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeCRUD == null)
            {
                return NotFound();
            }

            return View(employeeCRUD);
        }

        // GET: Main/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Main/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Login,LastUpdate,Hours,Status")] EmployeeCRUD employeeCRUD)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeCRUD);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeCRUD);
        }

        // GET: Main/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeCRUD == null)
            {
                return NotFound();
            }

            var employeeCRUD = await _context.EmployeeCRUD.FindAsync(id);
            if (employeeCRUD == null)
            {
                return NotFound();
            }
            return View(employeeCRUD);
        }

        // POST: Main/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Login,LastUpdate,Hours,Status")] EmployeeCRUD employeeCRUD)
        {
            if (id != employeeCRUD.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeCRUD);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeCRUDExists(employeeCRUD.Id))
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
            return View(employeeCRUD);
        }

        // GET: Main/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeCRUD == null)
            {
                return NotFound();
            }

            var employeeCRUD = await _context.EmployeeCRUD
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeCRUD == null)
            {
                return NotFound();
            }

            return View(employeeCRUD);
        }

        // POST: Main/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeCRUD == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EmployeeCRUD'  is null.");
            }
            var employeeCRUD = await _context.EmployeeCRUD.FindAsync(id);
            if (employeeCRUD != null)
            {
                _context.EmployeeCRUD.Remove(employeeCRUD);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeCRUDExists(int id)
        {
            return (_context.EmployeeCRUD?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
