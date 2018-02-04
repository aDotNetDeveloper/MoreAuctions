using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MoreAuctions.Models;
using Microsoft.Extensions.Options;
using System;

namespace MoreAuctions.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly MoreAuctionsContext _context;
        private IOptions<BusinessRules> _businessRules;
        private IEmailSender _emailSender;

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public AuctionsController(MoreAuctionsContext context, IEmailSender emailSender, IOptions<BusinessRules> businessRules)
        {
            _context = context;
            _businessRules = businessRules;
            _emailSender = emailSender;
        }

        // GET: Auctions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Auction.Include("Items").ToListAsync());
        }

        // GET: Auctions/About
        public IActionResult About()
        {
            return View();
        }

        // GET: Auctions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auction.SingleOrDefaultAsync(m => m.ID == id);

            if (auction == null)
            {
                return NotFound();
            }
            auction.Items = await _context.AuctionItem.Where(m => m.AuctionID == id).ToListAsync();

            return View(auction);
        }

        // GET: Auctions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Auctions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Description,Date")] Auction auction)
        {
            if (ModelState.IsValid && AuctionUnique(auction))
            {
                _context.Add(auction);
                await _context.SaveChangesAsync();
                await _emailSender.Send($"Auction created for {auction.Date.ToShortDateString()}", $"{auction.Description} has been created");
                return RedirectToAction(nameof(Index));
            }
            return View(auction);
        }

        // GET: Auctions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auction.SingleOrDefaultAsync(m => m.ID == id);
            if (auction == null)
            {
                return NotFound();
            }
            return View(auction);
        }

        // POST: Auctions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Description,Date")] Auction auction)
        {
            if (id != auction.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid && AuctionUnique(auction))
            {
                try
                {
                    _context.Update(auction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuctionExists(auction.ID))
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
            return View(auction);
        }

        // GET: Auctions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auction
                .SingleOrDefaultAsync(m => m.ID == id);
            if (auction == null)
            {
                return NotFound();
            }

            return View(auction);
        }

        // POST: Auctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auction = await _context.Auction.SingleOrDefaultAsync(m => m.ID == id);
            if (auction == null)
            {
                return NotFound();
            }
            _context.Auction.Remove(auction);
            await _context.SaveChangesAsync();
            await _emailSender.Send($"Auction cancelled for {auction.Date.ToShortDateString()}", $"{auction.Description} has been removed");
            return RedirectToAction(nameof(Index));
        }

        private bool AuctionExists(int id)
        {
            return _context.Auction.Any(e => e.ID == id);
        }

        private bool AuctionUnique(Auction auction)
        {
            var unique = !_context.Auction.Any(a => a.ID != auction.ID && a.Description.Equals(auction.Description, StringComparison.InvariantCultureIgnoreCase));
            if (!unique)
                ModelState.AddModelError("Description", "Description of auction must be unique");
            return unique;
        }

    }
}
