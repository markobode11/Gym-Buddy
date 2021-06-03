using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.DTO;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize]
    public class UserProgramsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public UserProgramsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: UserPrograms
        public async Task<IActionResult> Index()
        {
            var userId = User.GetUserId();
            if (userId == null) return NotFound();
            var userPrograms =  await _uow.UserPrograms.GetAllAsync(userId.Value);
            return View(userPrograms);
        }

        // GET: UserPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.GetUserId();
            if (userId == null) return NotFound();

            var userProgram = await _uow.UserPrograms.FirstOrDefaultAsync(id.Value, userId.Value);

            if (userProgram == null) return NotFound();

            return View(userProgram);
        }

        // GET: UserPrograms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserPrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserProgram userProgram)
        {
            if (!ModelState.IsValid) return View(userProgram);
            
            userProgram.AppUserId = User.GetUserId()!.Value;
            _uow.UserPrograms.Add(userProgram);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: UserPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var userId = User.GetUserId();
            if (userId == null) return NotFound();
            
            var userProgram = await _uow.UserPrograms.FirstOrDefaultAsync(id.Value, userId.Value);

            if (userProgram == null) return NotFound();

            return View(userProgram);
        }

        // POST: UserPrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserProgram userProgram)
        {
            if (id != userProgram.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    userProgram.AppUserId = User.GetUserId()!.Value;
                    _uow.UserPrograms.Update(userProgram);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.UserPrograms.ExistsAsync(userProgram.Id)) return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(userProgram);
        }

        // GET: UserPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var userId = User.GetUserId();
            if (userId == null) return NotFound();

            var userProgram = await _uow.UserPrograms.FirstOrDefaultAsync(id.Value, userId.Value);

            if (userProgram == null) return NotFound();

            return View(userProgram);
        }

        // POST: UserPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _uow.UserPrograms.RemoveAsync(id, User.GetUserId()!.Value);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}