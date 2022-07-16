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

        // GET: Main/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id.HasValue == false || id.Value == default(int))
                return NotFound();

            Employee user = null;

            _context.Users.SingleOrDefault(x => x.CardId == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Main/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Login,Hours,Status")] Employee users)
        {

            if (users == null) return NotFound("Entity 'Employee' is null.");
            
            Employee temp = default(Employee);
            
            foreach (var item in _context.Users)
            {
                if (item.CardId == Id)
                {
                    temp = item;
                    break;
                }
            }

            if (temp.LastUpdate.AddHours(20) > DateTime.Now) return NotFound("20 hours...");

            if (temp.Login != users.Login) return NotFound();
            temp.Login = users.Login;
            temp.Hours += users.Hours;
            temp.Status = users.Status;
            temp.Salary += temp.Hours * 50;
            temp.LastUpdate = DateTime.Now;

            _context.Users.Update(temp);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Main/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var employeeCRUD = await _context.Users
                .FirstOrDefaultAsync(m => m.CardId == id);
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
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EmployeeCRUD'  is null.");
            }
            var employeeCRUD = await _context.Users.FindAsync(id);
            if (employeeCRUD != null)
            {
                _context.Users.Remove(employeeCRUD);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
