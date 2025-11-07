using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab1.Controllers
{
    [Route("Admin/[controller]")]
    public class StudentController : Controller
    {
        private List<Student> ListStudents = new List<Student>();
        public StudentController()
        {
            ListStudents = new List<Student>()
            {
                new Student(){ Id = 101, Name = "Hai Nam", Branch = Branch.IT,
                Gender = Gender.Male, IsRegular = true, Address="A1-2018", Email = "abc@f.com"},
                new Student(){ Id = 102, Name = "Thi Thu", Branch = Branch.BE, Gender = Gender.Male,
                IsRegular = false, Address="B2-2018", Email = "s@ds.com" },
                new Student(){ Id = 103, Name = "Van An", Branch = Branch.CE,
                Gender = Gender.Male,IsRegular = false, Address="C3-2018", Email = "ss@f.com"},
                new Student(){ Id = 104, Name = "Thi Binh", Branch = Branch.EE,Gender = Gender.Female,
                IsRegular = false, Address="D4-2018", Email = "sdas@f.com" }
            };
        }
        [HttpGet("List")]
        public IActionResult Index()
        {
            return View(ListStudents);
        }
        [HttpGet("Add")]
        public IActionResult Create()
        {
            //Lấy danh sách các giá trij Gender để hiển thị radio button tren form
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            //Lấy danh sách các giá trij Branch để hiển thị select-option tren form
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text="IT", Value="1"},
                new SelectListItem {Text="BE", Value="2"},
                new SelectListItem {Text="CE", Value = "3"},
                new SelectListItem {Text="EE", Value = "4"}
            };
            return View();
        }
        [HttpPost("Add")]
        public IActionResult Create(Student s)
        {
            s.Id = ListStudents.Last().Id + 1;
            ListStudents.Add(s);

            return View("Index", ListStudents);
        }
    }
}
