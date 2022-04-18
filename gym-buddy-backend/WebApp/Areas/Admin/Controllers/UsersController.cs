using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.DTO.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
#pragma warning disable 1591

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IAppUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(IAppUnitOfWork uow, UserManager<AppUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _uow.AppUsers.GetAllAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var appUser = await _uow.AppUsers.FirstOrDefaultAsync(id.Value);
            if (appUser == null) return NotFound();

            return View(appUser);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUser appUser)
        {
            if (!ModelState.IsValid) return View(appUser);

            _uow.AppUsers.Add(appUser);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var appUser = await _uow.AppUsers.FirstOrDefaultAsync(id.Value);
            if (appUser == null) return NotFound();

            return View(appUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AppUser appUser)
        {
            // TODO: admins ability to change roles
            // var user = await _uow.AppUsers.FirstOrDefaultAsync(id);
            //
            // await _userManager.RemoveFromRoleAsync(appUser!, user!.CurrentRole!.Name);
            // await _userManager.AddToRoleAsync(appUser!, appUser.CurrentRole!.Name);
            //
            // user!.CurrentRole = appUser.CurrentRole;
            // _uow.AppUsers.Update(user);
            // await _uow.SaveChangesAsync();
            //
            // return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var appUser = await _uow.AppUsers.FirstOrDefaultAsync(id.Value);
            if (appUser == null) return NotFound();

            return View(appUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _uow.AppUsers.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Ban(int? id)
        {
            if (id == null) return NotFound();

            var appUser = await _uow.AppUsers.FirstOrDefaultAsync(id.Value);
            if (appUser == null) return NotFound();

            return View(appUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Ban(int id, AppUser appUser)
        {
            if (id != appUser.Id) return NotFound();


            return View(appUser);
        }
    }
}