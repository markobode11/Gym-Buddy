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
    public class ExercisesController : Microsoft.AspNetCore.Mvc.Controller
    {
        // private readonly ExerciseRepository _repository;
        private readonly IAppUnitOfWork _uow;

        public ExercisesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Exercises
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Exercises.GetAllAsync());
        }

        // GET: Exercises/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var exercise = await _uow.Exercises.FirstOrDefaultAsync(id.Value);
            return exercise == null ? NotFound() : View(exercise);
        }

        // GET: Exercises/Create
        public async Task<IActionResult> Create()
        {
            ViewData["DifficultyId"] =
                new SelectList(await _uow.Difficulties.GetAllAsync(),
                    "Id",
                    "Name");
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,DifficultyId")]
            Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                _uow.Exercises.Add(exercise);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DifficultyId"] = await DifficultiesSelectList(exercise);
            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var exercise = await _uow.Exercises.FirstOrDefaultAsync(id.Value);
            if (exercise == null) return NotFound();

            ViewData["DifficultyId"] = await DifficultiesSelectList(exercise);

            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,DifficultyId")]
            Exercise exercise)
        {
            if (id != exercise.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Exercises.Update(exercise);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Exercises.ExistsAsync(exercise.Id)) return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["DifficultyId"] = await DifficultiesSelectList(exercise);

            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var exercise = await _uow.Exercises.FirstOrDefaultAsync(id.Value);

            return exercise == null ? NotFound() : View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _uow.Exercises.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<SelectList> DifficultiesSelectList(Exercise exercise)
        {
            return new(
                await _uow.Difficulties.GetAllAsync(),
                "Id", "Name",
                exercise.Difficulty!.Id);
        }
    }
}