using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Data;
using Hospital.Models;

namespace Hospital.Controllers
{
    public class MedicationsController : Controller
    {
        private readonly ChdbContext _context;

        public MedicationsController(ChdbContext context)
        {
            _context = context;
        }

        // GET: Medications
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.DescriptionSortParm = string.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
            ViewBag.CostSortParm = sortOrder == "cost" ? "cost_desc" : "cost";
            ViewBag.SearchString = searchString;
            var medications = from m
                              in _context.Medications
                              select m;
            if (!string.IsNullOrEmpty(searchString)) {
                medications = medications.Where(m => m.MedicationDescription.ToLower().Contains(searchString.ToLower()));
            }
            switch (sortOrder) {
                case "description_desc":
                medications = medications.OrderByDescending(m => m.MedicationDescription);
                break;
                case "cost":
                medications = medications.OrderBy(m => m.MedicationCost);
                break;
                case "cost_desc":
                medications = medications.OrderByDescending(m => m.MedicationCost);
                break;
                default:
                medications = medications.OrderBy(m => m.MedicationDescription);
                break;
            }

            return View(await medications.AsNoTracking().ToListAsync());
        }

        // GET: Medications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medications
                .FirstOrDefaultAsync(m => m.MedicationId == id);
            if (medication == null)
            {
                return NotFound();
            }

            return View(medication);
        }

        // GET: Medications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicationId,MedicationDescription,MedicationCost,PackageSize,Strength,Sig,UnitsUsedYtd,LastPrescribedDate")] Medication medication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medication);
        }

        // GET: Medications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medications.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }
            return View(medication);
        }

        // POST: Medications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicationId,MedicationDescription,MedicationCost,PackageSize,Strength,Sig,UnitsUsedYtd,LastPrescribedDate")] Medication medication)
        {
            if (id != medication.MedicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicationExists(medication.MedicationId))
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
            return View(medication);
        }

        // GET: Medications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medications
                .FirstOrDefaultAsync(m => m.MedicationId == id);
            if (medication == null)
            {
                return NotFound();
            }

            return View(medication);
        }

        // POST: Medications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try {
                if(_context.Medications == null) {
                    return Problem("Entity set 'ChdbContext.Medications' is null");
                }
                var medication = await _context.Medications.FindAsync(id);
                if (medication != null) {
                    _context.Medications.Remove(medication);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } catch {
                var medication = await _context.Medications.FindAsync(id);
                if (medication != null) {
                    ViewBag.ErrorMessage = $"Unable to delete {medication.MedicationDescription}. It is referenced by other data in the system";
                } else {
                    ViewBag.ErrorMessage = $"Unable to delete medication id {id}. It is referenced by other data in the system.";
                }

                return View("Error", new ErrorViewModel());
            }
        }

        private bool MedicationExists(int id)
        {
            return _context.Medications.Any(e => e.MedicationId == id);
        }
    }
}
