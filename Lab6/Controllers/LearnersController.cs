using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab5.Models;

namespace Lab5.Controllers
{
    public class LearnersController : Controller
    {
        private Lab4CodeFirstContext db;

        public LearnersController(Lab4CodeFirstContext context)
        {
            db = context;
        }

        // GET: Learners
        public IActionResult LearnerByMajorId(int mid)
        {
            var learners = db.Learners.Where(l => l.MajorId == mid)
                                   .Include(l => l.Major).ToList();
          
            return PartialView("LearnerTable",learners);
        }
        private int pageSize = 3;
        public async Task<IActionResult> Index(int? mid)
        {
            var learners = db.Learners.Include(m => m.Major);

            if (mid != null)
            {
                learners = db.Learners
                             .Where(l => l.MajorId == mid)
                             .Include(m => m.Major);
            }

            // Tính số trang bằng async
            int pageNum = (int)Math.Ceiling((double)await learners.CountAsync() / pageSize);

            ViewBag.PageNum = pageNum;

            // Lấy dữ liệu trang đầu bằng async
            var result = await learners.Take(pageSize).ToListAsync();

            return View(result);
        }
        public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
        {
            // Lấy toàn bộ learners (IQueryable để build query linh hoạt)
            IQueryable<Learner> learners = db.Learners.Include(l => l.Major);

            // Nếu trang null thì mặc định = 1
            int page = (pageIndex == null || pageIndex <= 0) ? 1 : pageIndex.Value;

            // Nếu có mid thì lọc theo Major
            if (mid != null)
            {
                learners = learners.Where(l => l.MajorId == mid);
                ViewBag.mid = mid;
            }

            // Nếu có keyword thì tìm theo tên (FirstMidName hoặc LastName tuỳ bạn)
            if (!string.IsNullOrEmpty(keyword))
            {
                string kw = keyword.ToLower();
                learners = learners.Where(l =>
                    l.FirstMidName.ToLower().Contains(kw) ||
                    l.LastName.ToLower().Contains(kw)
                );

                ViewBag.keyword = keyword;
            }

            // Tính tổng số trang
            int totalRows = learners.Count();
            int pageNum = (int)Math.Ceiling((float)totalRows / pageSize);
            ViewBag.pageNum = pageNum;

            // Chọn dữ liệu trang hiện tại
            var result = learners
                            .Skip(pageSize * (page - 1))
                            .Take(pageSize)
                            .ToList();

            // Gửi lại chỉ số trang hiện tại
            ViewBag.pageIndex = page;

            return PartialView("LearnerTable", result);
        }


        // GET: Learners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learner = await db.Learners
                .Include(l => l.Major)
                .FirstOrDefaultAsync(m => m.LearnerId == id);
            if (learner == null)
            {
                return NotFound();
            }

            return View(learner);
        }

        // GET: Learners/Create
        public IActionResult Create()
        {
            ViewData["MajorId"] = new SelectList(db.Majors, "MajorId", "MajorId");
            return View();
        }

        // POST: Learners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LearnerId,LastName,FirstMidName,EnrollmentDate,MajorId")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Add(learner);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MajorId"] = new SelectList(db.Majors, "MajorId", "MajorId", learner.MajorId);
            return View(learner);
        }

        // GET: Learners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learner = await db.Learners.FindAsync(id);
            if (learner == null)
            {
                return NotFound();
            }
            ViewData["MajorId"] = new SelectList(db.Majors, "MajorId", "MajorId", learner.MajorId);
            return View(learner);
        }

        // POST: Learners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LearnerId,LastName,FirstMidName,EnrollmentDate,MajorId")] Learner learner)
        {
            if (id != learner.LearnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(learner);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(learner.LearnerId))
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
            ViewData["MajorId"] = new SelectList(db.Majors, "MajorId", "MajorId", learner.MajorId);
            return View(learner);
        }

        // GET: Learners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learner = await db.Learners
                .Include(l => l.Major)
                .FirstOrDefaultAsync(m => m.LearnerId == id);
            if (learner == null)
            {
                return NotFound();
            }

            return View(learner);
        }

        // POST: Learners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learner = await db.Learners.FindAsync(id);
            if (learner != null)
            {
                db.Learners.Remove(learner);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LearnerExists(int id)
        {
            return db.Learners.Any(e => e.LearnerId == id);
        }
    }
}
