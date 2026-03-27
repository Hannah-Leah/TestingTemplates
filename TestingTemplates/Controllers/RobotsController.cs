using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestingTemplates.Models;

namespace TestingTemplates.Controllers
{
    public class RobotsController : Controller
    {
        private readonly AspNet26Context _context;

        public RobotsController(AspNet26Context context)
        {
            _context = context;
        }

        // GET: Robots
        public async Task<IActionResult> Index()
        {
            return View(await _context.Robots.ToListAsync());
        }

        // GET: Robots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var robot = await _context.Robots
                .FirstOrDefaultAsync(m => m.RobidPk == id);
            if (robot == null)
            {
                return NotFound();
            }

            return View(robot);
        }

        // GET: Robots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Robots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RobidPk,RobDescription,RobType,RobColor,RobStatus")] Robot robot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(robot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(robot);
        }

        // GET: Robots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var robot = await _context.Robots.FindAsync(id);
            if (robot == null)
            {
                return NotFound();
            }
            return View(robot);
        }

        // POST: Robots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RobidPk,RobDescription,RobType,RobColor,RobStatus")] Robot robot)
        {
            if (id != robot.RobidPk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(robot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RobotExists(robot.RobidPk))
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
            return View(robot);
        }

        // GET: Robots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var robot = await _context.Robots
                .FirstOrDefaultAsync(m => m.RobidPk == id);
            if (robot == null)
            {
                return NotFound();
            }

            return View(robot);
        }

        // POST: Robots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var robot = await _context.Robots.FindAsync(id);
            if (robot != null)
            {
                _context.Robots.Remove(robot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RobotExists(int id)
        {
            return _context.Robots.Any(e => e.RobidPk == id);
        }
    }
}
