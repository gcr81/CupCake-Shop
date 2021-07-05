using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CupCakeShop.Data;
using CupCakeShop.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;

/// <summary>
/// Creted on :7/1/2021 10:24 Am
/// Last changes made: 7/5/2021 12:04 am 
/// </summary>

namespace CupCakeShop.Controllers
{
    public class CakesController : Controller
    {
        private readonly ApplicationDbContext _context;

        #region  GET Details
        public IActionResult HomePage()
        {
            return View();
        }

        public CakesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public new async Task<IActionResult> ViewData()
        {
            return View(await _context.CupCake.ToListAsync());
        }

        // GET: Cakes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CupCake.ToListAsync());
        }

        // GET: Cakes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cake = await _context.CupCake
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cake == null)
            {
                return NotFound();
            }

            return View(cake);
        }


     #endregion

        #region GET/POST - Create
        [Authorize]
        // GET: Cakes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CupCakeName,CupCakePrice,ID,CreatedByUser,CreatedDate,ModifiedByUser,ModifiedDate")] Cake cake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cake);
        }
        #endregion

        #region GET/POST Edit
        // GET: Cakes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cake = await _context.CupCake.FindAsync(id);

            if (cake == null)
            {
                return NotFound();
            }
            return View(cake);
        }


        // POST: Cakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CupCakeName,CupCakePrice,ID,CreatedByUser,CreatedDate,ModifiedByUser,ModifiedDate")] Cake cake)
        {
            if (id != cake.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cake);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = string.Format("Your data has been edited");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CakeExists(cake.ID))
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
            return View(cake);
        }
        #endregion

        #region  GET/POST Delete

        // GET: Cakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cake = await _context.CupCake
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cake == null)
            {
                return NotFound();
            }

            return View(cake);
        }

        // POST: Cakes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cake = await _context.CupCake.FindAsync(id);
            _context.CupCake.Remove(cake);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CakeExists(int id)
        {
            return _context.CupCake.Any(e => e.ID == id);
        }
        #endregion

        #region  Get & Post - Search Form
        /// <summary>
        /// Created a Function which is used on the search view.
        /// The fuction search bases on letter found on cupcakeName 
        /// </summary>
        // GET: Cakes/SearchForm
        /// <returns></returns>
        public IActionResult ShowSearchForm()
        {
            return View();
        }

        // Post: Cakes.ShowSearchResult
        // Created 2 searched :
        // One for the Index View and second for ViewData
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            if (SearchPhrase != "" )
            return View("Index", await _context.CupCake.Where(j => j.CupCakeName.Contains(SearchPhrase)).ToListAsync());
            else
                return View(await _context.CupCake.ToListAsync());
        }

        //The second search for View data//
        //Same algorithm as the previous one 
        public async Task<IActionResult> ShowSearchResultsCard(String SearchPhrase)
        {
            if (SearchPhrase != "")
                return View("ViewData", await _context.CupCake.Where(j => j.CupCakeName.Contains(SearchPhrase)).ToListAsync());
            else
                return View(await _context.CupCake.ToListAsync());
           
        }


        #endregion

    }
}
