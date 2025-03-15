using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.Service.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    //[JwtAuthorize]
    public class HomeController : Controller
    {

        private readonly IStudentService _studentService;

        public HomeController(IStudentService studentService)
        {
            _studentService = studentService;
        }



        public async Task<IActionResult> Index()
        {
            var jwtToken = HttpContext.Request.Cookies["jwtToken"];

            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Login", "Account"); // Token yoksa login sayfasına yönlendir
            }


            var students= await _studentService.GetAllStudents();
            return View(students);
        }


        #region Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateVM studentCreateVM)
        {
            var foto =await Helper.ConvertIFormFileToByteArray(studentCreateVM.Photo);

            Student student = new Student() 
            { 
            Name= studentCreateVM.Name,
            Surname= studentCreateVM.Surname,
            Father= studentCreateVM.Father,
            Mother= studentCreateVM.Mother,
            Address= studentCreateVM.Address,
            Gender= studentCreateVM.Gender,
            FIN=studentCreateVM.FIN,
            MobileTel= studentCreateVM.MobileTel,
            Foto=foto,
            };
            await _studentService.CreateStudentAsync(student);
            return RedirectToAction("Index", "Home");
        }
        #endregion



        #region Edit
        public async Task<IActionResult> Edit(int Id)
        {
            var studentCreateVM = await _studentService.GetStudentAsync(Id);

            if (studentCreateVM == null) return NotFound();

            StudentEditVM student = new StudentEditVM()
            {
                Name = studentCreateVM.Name,
                Surname = studentCreateVM.Surname,
                Father = studentCreateVM.Father,
                Mother = studentCreateVM.Mother,
                Address = studentCreateVM.Address,
                Gender = studentCreateVM.Gender,
                FIN = studentCreateVM.FIN,
                MobileTel = studentCreateVM.MobileTel,
                Foto = studentCreateVM.Foto,
            };
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, StudentEditVM studentEditVM)
        {

            if (Id == null) return BadRequest();

		
            var student = await _studentService.GetStudentAsync(Id);
           student.Surname = studentEditVM.Surname;
            student.FIN = studentEditVM.FIN;
            student.Name = studentEditVM.Name;
            student.Address = studentEditVM.Address;
            student.Gender = studentEditVM.Gender;  
            student.Father = studentEditVM.Father;
            student.MobileTel = studentEditVM.MobileTel;
            student.Mother = studentEditVM.Mother;
            if (studentEditVM.Photo != null)
            {
                var foto = await Helper.ConvertIFormFileToByteArray(studentEditVM.Photo);
                student.Foto = foto;
            }
           
			await _studentService.UpdateStudentAsync(student);

            return RedirectToAction("Index", "Home");

        }

        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {

            if (Id == null) return BadRequest();

            
            await _studentService.DeleteStudentAsync(Id);

            return RedirectToAction("Index", "Home");
        }
        #endregion


    }
}
