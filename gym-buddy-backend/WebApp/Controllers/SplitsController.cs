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
    public class SplitsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public SplitsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Splits
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Splits.GetAllAsync());
        }

        // GET: Splits/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        
        {
            if (id == null) return NotFound();

            var split = await _uow.Splits.FirstOrDefaultAsync(id.Value);
            
            return split == null ? NotFound() : View(split);
        }

        // GET: Splits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Splits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Split split)
        {
            if (!ModelState.IsValid) return View(split);

            _uow.Splits.Add(split);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Splits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var split = await _uow.Splits.FirstOrDefaultAsync(id.Value);
            
            return split == null ? NotFound() : View(split);
        }

        // POST: Splits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Split split)
        {
            if (id != split.Id) return NotFound();

            if (!ModelState.IsValid) return View(split);
            
            try
            {
                _uow.Splits.Update(split);
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Splits.ExistsAsync(split.Id)) return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: Splits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            
            var split = await _uow.Splits.FirstOrDefaultAsync(id.Value);
            
            return split == null ? NotFound() : View(split);
        }

        // POST: Splits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _uow.Splits.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}