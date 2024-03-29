﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Edit(int? cardId)
        {
            if (cardId.HasValue == false || cardId.Value == default(int))
            {
                return NotFound();
            }

            var user = _context.Users.SingleOrDefault(x => x.CardId == cardId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Main/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int cardId, [Bind("Login,Hours,Status")] Employee user)
        {

            if (user == null) return NotFound("Entity 'Employee' is null.");

            var temp = _context.Users.SingleOrDefault(x => x.CardId == cardId);

            if (temp.LastUpdate.AddHours(20) > DateTime.Now) return NotFound("20 hours...");

            if (temp.Login != user.Login) return NotFound();
            temp.Login = user.Login;
            temp.Hours += user.Hours;
            temp.Status = user.Status;
            temp.Salary += temp.Hours * 50;
            temp.LastUpdate = DateTime.Now;

            _context.Users.Update(temp);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Main/Delete/5
        public async Task<IActionResult> Delete(int? cardId)
        {
            if (cardId.HasValue == false || cardId.Value == default(int))
                return NotFound();

            var employeeCRUD = await _context.Users
                .FirstOrDefaultAsync(m => m.CardId == cardId);

            if (employeeCRUD == null)
            {
                return NotFound();
            }

            return View(employeeCRUD);
        }

        // POST: Main/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int cardId)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EmployeeCRUD'  is null.");
            }
            var employeeCRUD = await _context.Users.FindAsync(cardId);
            if (employeeCRUD != null)
            {
                _context.Users.Remove(employeeCRUD);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
