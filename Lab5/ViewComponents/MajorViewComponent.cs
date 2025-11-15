using Microsoft.AspNetCore.Mvc;
using Lab5.Models;
namespace Lab5.ViewComponents
{
    public class MajorViewComponent: ViewComponent
    {
        Lab4CodeFirstContext db;
        List<Major> majors;
        public MajorViewComponent(Lab4CodeFirstContext _context)
        {
            db = _context;
            majors = db.Majors.ToList();
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderMajor",majors);
        }
    }
}
