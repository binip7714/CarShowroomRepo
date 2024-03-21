using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BMCar.Data;
using BMCar.Models;
using Microsoft.AspNetCore.Authorization;

namespace BMCar.Controllers
{
    [Authorize]
    public class CarLeadController : Controller
    {
        private readonly CarApplicationDbContext _context;

        public CarLeadController(CarApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarLead
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarLead.ToListAsync());
        }

        // GET: CarLead/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carLeadEntity = await _context.CarLead
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carLeadEntity == null)
            {
                return NotFound();
            }

            return View(carLeadEntity);
        }

        // GET: CarLead/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarLead/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,Model,Details,Price")] CarLeadEntity carLeadEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carLeadEntity);
                await _context.SaveChangesAsync();
                TempData["success"] = "Data Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(carLeadEntity);
        }

        // GET: CarLead/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carLeadEntity = await _context.CarLead.FindAsync(id);
            if (carLeadEntity == null)
            {
                return NotFound();
            }
            return View(carLeadEntity);
        }

        // POST: CarLead/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model,Details,Price")] CarLeadEntity carLeadEntity)
        {
            if (id != carLeadEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carLeadEntity);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Data Updated Successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarLeadEntityExists(carLeadEntity.Id))
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
            return View(carLeadEntity);
        }

        // GET: CarLead/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carLeadEntity = await _context.CarLead
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carLeadEntity == null)
            {
                return NotFound();
            }

            return View(carLeadEntity);
        }

        // POST: CarLead/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carLeadEntity = await _context.CarLead.FindAsync(id);
            if (carLeadEntity != null)
            {
                _context.CarLead.Remove(carLeadEntity);
            }

            await _context.SaveChangesAsync();
            TempData["success"] = "Data Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool CarLeadEntityExists(int id)
        {
            return _context.CarLead.Any(e => e.Id == id);
        }
    }
}
