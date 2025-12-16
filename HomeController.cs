using Microsoft.AspNetCore.Mvc;
using labdemo1.Model;

namespace labdemo1.Controllers
{
    public class HomeController : Controller
    {

        StudentViewModel svm  = new StudentViewModel();
        public IActionResult Index()
        {
            List<Student> students = svm.GetStudents();

            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                svm.AddStudent(student);
                return Redirect("/Home/Index");
            }
            else
            {
                return View();
            }
                
        }

        public IActionResult Edit(int id)
        {
            Student students = svm.GetStudents(id);
            return View(students);
        }

        [HttpPost]
        public IActionResult Edit(Student updateStudent)
        {
            if (ModelState.IsValid)
            {
                int rowsAffected = svm.UpdateStudent(updateStudent);
                if (rowsAffected > 0)
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    ViewBag.Message = "Data not updated";
                    return View(updateStudent);
                }
            }
            else
            {
                ViewBag.Message = "Something wrong!";
                return View(updateStudent);
            }
            
        }

        public IActionResult Delete(int id)
        {
            svm.RemoveStudent(id);
            return Redirect("/Home/Index");

        }



    }
}
