using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Collectors.Data;
using Collectors.Models;
using Collectors.Models.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace Collectors.Controllers
{
    public class CollectiblesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CollectiblesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Collectibles
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var applicationDbContext = _context.Collectibles.Include(c => c.Collection).Where(c => c.Collection.UserId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Collectibles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectible = await _context.Collectibles
                .Include(c => c.Collection)
                .Include(c => c.CollectibleTags)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collectible == null)
            {
                return NotFound();
            }

            return View(collectible);
        }

        // GET: Collectibles/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var viewModel = new CollectibleCreateViewModel()
            {
                Collections = await _context.Collections.Where(c => c.UserId == user.Id).ToListAsync(),
                Tags = await _context.Tags.ToListAsync()
            };
            return View(viewModel);
        }

        // POST: Collectibles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CollectibleCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Collectible.CollectedDate = DateTime.Today;
                _context.Add(viewModel.Collectible);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Collectibles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var viewModel = new CollectibleEditViewModel()
            {
                Collections = await _context.Collections.Where(c => c.User.Id == user.Id).ToListAsync(),
                Tags = await _context.Tags.ToListAsync()
            };

            var collectible = await _context.Collectibles.FindAsync(id);
            if (collectible == null)
            {
                return NotFound();
            }
            else
            {
                viewModel.Collectible = collectible;
            }
            return View(viewModel);
        }

        // POST: Collectibles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CollectibleEditViewModel viewModel)
        {
            if (id != viewModel.Collectible.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    viewModel.Collectible.CollectibleTags = viewModel.SelectedTagIds.Select(t => new CollectibleTag()
                    {
                        TagId = t,
                        CollectibleId = id
                    }).ToList();
                    _context.Update(viewModel.Collectible);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectibleExists(viewModel.Collectible.Id))
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
            return View(viewModel);
        }

        // GET: Collectibles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectible = await _context.Collectibles
                .Include(c => c.Collection)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collectible == null)
            {
                return NotFound();
            }

            return View(collectible);
        }

        // POST: Collectibles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collectible = await _context.Collectibles.FindAsync(id);
            _context.Collectibles.Remove(collectible);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectibleExists(int id)
        {
            return _context.Collectibles.Any(e => e.Id == id);
        }
    }
}
