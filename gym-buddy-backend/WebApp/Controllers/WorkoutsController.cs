using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.DTO;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WorkoutsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public WorkoutsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Workouts
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Workouts.GetAllAsync());
        }

        // GET: Workouts/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var workout = await _uow.Workouts.FirstOrDefaultAsync(id.Value);
            return workout == null ? NotFound() : View(workout);
        }

        // GET: Workouts/Create
        public async Task<IActionResult> Create()
        {
            ViewData["DifficultyId"] = new SelectList(await _uow.Difficulties.GetAllAsync(),
                "Id",
                "Name");
            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Duration,DifficultyId")]
            Workout workout)
        {
            if (ModelState.IsValid)
            {
                _uow.Workouts.Add(workout);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DifficultyId"] = await DifficultiesSelectList(workout);
            return View(workout);
        }

        // GET: Workouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var workout = await _uow.Workouts.FirstOrDefaultAsync(id.Value);
            if (workout == null) return NotFound();

            ViewData["DifficultyId"] = await DifficultiesSelectList(workout);
            return View(workout);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Duration,DifficultyId")]
            Workout workout)
        {
            if (id != workout.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Workouts.Update(workout);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Workouts.ExistsAsync(workout.Id)) return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["DifficultyId"] = await DifficultiesSelectList(workout);
            return View(workout);
        }

        // GET: Workouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var workout = await _uow.Workouts.FirstOrDefaultAsync(id.Value);

            return workout == null ? NotFound() : View(workout);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _uow.Workouts.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<SelectList> DifficultiesSelectList(Workout workout)
        {
            return new(
                await _uow.Difficulties.GetAllAsync(),
                "Id",
                "Name",
                workout.Difficulty!.Id);
        }
    }
}