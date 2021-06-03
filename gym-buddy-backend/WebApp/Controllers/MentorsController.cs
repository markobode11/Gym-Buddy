using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.DTO;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MentorsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public MentorsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Mentors
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Mentors.GetAllAsync());
        }

        // GET: Mentors/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var mentor = await _uow.Mentors.FirstOrDefaultAsync(id.Value);

            return mentor == null ? NotFound() : View(mentor);
        }

        // GET: Mentors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mentors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Specialty,Since,Description,Id")]
            Mentor mentor)
        {
            if (!ModelState.IsValid) return View(mentor);

            _uow.Mentors.Add(mentor);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Mentors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var mentor = await _uow.Mentors.FirstOrDefaultAsync(id.Value);

            return mentor == null ? NotFound() : View(mentor);
        }

        // POST: Mentors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Specialty,Since,Description,Id")]
            Mentor mentor)
        {
            if (id != mentor.Id) return NotFound();

            if (!ModelState.IsValid) return View(mentor);
            
            try
            {
                _uow.Mentors.Update(mentor);
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Mentors.ExistsAsync(mentor.Id)) return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: Mentors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var mentor = await _uow.Mentors.FirstOrDefaultAsync(id.Value);
            
            return mentor == null ? NotFound() : View(mentor);
        }

        // POST: Mentors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _uow.Mentors.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}