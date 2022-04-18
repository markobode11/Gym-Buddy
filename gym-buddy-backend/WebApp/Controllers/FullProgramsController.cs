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
    public class FullProgramsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IAppUnitOfWork _uow;

        public FullProgramsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: FullPrograms
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Programs.GetAllAsync());
        }

        // GET: FullPrograms/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var fullProgram = await _uow.Programs.FirstOrDefaultAsync(id.Value);

            return fullProgram == null ? NotFound() : View(fullProgram);
        }

        // GET: FullPrograms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FullPrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Goal,Description")] FullProgram fullProgram)
        {
            if (!ModelState.IsValid) return View(fullProgram);

            _uow.Programs.Add(fullProgram);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: FullPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var fullProgram = await _uow.Programs.FirstOrDefaultAsync(id.Value);

            return fullProgram == null ? NotFound() : View(fullProgram);
        }

        // POST: FullPrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Goal,Description")] FullProgram fullProgram)
        {
            if (id != fullProgram.Id) return NotFound();

            if (!ModelState.IsValid) return View(fullProgram);
            
            try
            {
                _uow.Programs.Update(fullProgram);
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Programs.ExistsAsync(fullProgram.Id)) return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: FullPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var fullProgram = await _uow.Programs.FirstOrDefaultAsync(id.Value);
            
            return fullProgram == null ? NotFound() : View(fullProgram);
        }

        // POST: FullPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _uow.Programs.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}