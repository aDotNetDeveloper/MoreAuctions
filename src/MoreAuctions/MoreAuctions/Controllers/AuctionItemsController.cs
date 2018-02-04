using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreAuctions.Models;

namespace MoreAuctions.Controllers
{
    public class AuctionItemsController : Controller
    {
        private readonly MoreAuctionsContext _context;

        public AuctionItemsController(MoreAuctionsContext context)
        {
            _context = context;
        }

        // GET: AuctionItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.AuctionItem.ToListAsync());
        }

        // GET: AuctionItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionItem = await _context.AuctionItem
                .SingleOrDefaultAsync(m => m.ID == id);
            if (auctionItem == null)
            {
                return NotFound();
            }

            return View(auctionItem);
        }

        // GET: AuctionItems/Create
        public IActionResult Create(int id)
        {
            return View(new AuctionItem { AuctionID = id });
        }

        // POST: AuctionItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuctionID,Title,Description,StartPrice")] AuctionItem auctionItem)
        {
            if (ModelState.IsValid && AuctionItemUnique(auctionItem))
            {
                _context.Add(auctionItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Auctions", new { id = auctionItem.AuctionID });
            }
            return View(auctionItem);
        }

        // GET: AuctionItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionItem = await _context.AuctionItem.SingleOrDefaultAsync(m => m.ID == id);
            if (auctionItem == null)
            {
                return NotFound();
            }
            return View(auctionItem);
        }

        // POST: AuctionItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AuctionID,Title,Description,StartPrice")] AuctionItem auctionItem)
        {
            if (id != auctionItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid && AuctionItemUnique(auctionItem))
            {
                try
                {
                    _context.Update(auctionItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuctionItemExists(auctionItem.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Auctions", new { id = auctionItem.AuctionID });
            }
            return View(auctionItem);
        }

        // GET: AuctionItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionItem = await _context.AuctionItem
                .SingleOrDefaultAsync(m => m.ID == id);
            if (auctionItem == null)
            {
                return NotFound();
            }

            return View(auctionItem);
        }

        // POST: AuctionItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auctionItem = await _context.AuctionItem.SingleOrDefaultAsync(m => m.ID == id);
            _context.AuctionItem.Remove(auctionItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Auctions", new { id = auctionItem.AuctionID });
        }

        private bool AuctionItemExists(int id)
        {
            return _context.AuctionItem.Any(e => e.ID == id);
        }
 
        private bool AuctionItemUnique(AuctionItem auctionItem)
        {
            var unique = !_context.AuctionItem.Any(a => a.AuctionID == auctionItem.AuctionID && a.ID != auctionItem.ID && a.Title.Equals(auctionItem.Title, StringComparison.InvariantCultureIgnoreCase));
            if (!unique)
                ModelState.AddModelError("Title", "Title of item must be unique within each Auction");
            return unique;
        }
    }
}
