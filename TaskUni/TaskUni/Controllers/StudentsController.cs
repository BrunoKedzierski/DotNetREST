using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskUni.Model;

namespace TaskUni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        static List<String> list = new List<string>();





        [HttpGet]
        public async Task<IActionResult> GetStudentList()
        {
            StudentDAO StudentDAO = new StudentDAO("E:\\Desktop\\cwiczenia3_jd-BrunoKedzierski\\TaskUni\\TaskUni\\dane.csv");

            await StudentDAO.LoadStudentData();
            

            return Ok(StudentDAO.GetAllStudents());

        }

        [HttpGet("{Index}")]
        public async Task<IActionResult> GetStudentByIndex(string Index) {

            StudentDAO StudentDAO = new StudentDAO("E:\\Desktop\\cwiczenia3_jd-BrunoKedzierski\\TaskUni\\TaskUni\\dane.csv");

            await StudentDAO.LoadStudentData();


          
            return Ok(StudentDAO.GetStudentById(Index));
        
        
        }

    }


}
