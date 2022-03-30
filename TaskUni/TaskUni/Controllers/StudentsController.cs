using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskUni.Exceptions;
using TaskUni.Model;

namespace TaskUni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult> GetStudentList()
        {
            StudentDAO StudentDAO = new StudentDAO("E:\\Desktop\\cwiczenia3_jd-BrunoKedzierski\\TaskUni\\TaskUni\\dane.csv");

            await StudentDAO.LoadStudentData();
            

            return Ok(StudentDAO.GetAllStudents());

        }

        [HttpGet("{index}")]
        public async Task<IActionResult> GetStudentByIndex(string index) {

            StudentDAO StudentDAO = new StudentDAO("E:\\Desktop\\cwiczenia3_jd-BrunoKedzierski\\TaskUni\\TaskUni\\dane.csv");

            await StudentDAO.LoadStudentData();

            Student st = null;

            try
            {
                st = StudentDAO.GetStudentById(index);
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
    
          
            return Ok(st);
        
        
        }

        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] Student student) {

            StudentDAO StudentDAO = new StudentDAO("E:\\Desktop\\cwiczenia3_jd-BrunoKedzierski\\TaskUni\\TaskUni\\dane.csv");

            await StudentDAO.LoadStudentData();

            try
            {
                await StudentDAO.AddStudentAsync(student);

            }
            catch (DuplicatedStudentIdException ex)
            {

                return BadRequest(ex.Message);

            }
            catch (InvalidFormatException ex)
            {
                return BadRequest(ex.Message);

            }


            string uri = $"~/api/students{student.NumerIndeksu}";

            return Created(uri, student);

        }


        [HttpDelete("{index}")]
        public async Task<IActionResult> DeleteStudent(string index)
        {

            StudentDAO studentDAO = new StudentDAO("E:\\Desktop\\cwiczenia3_jd-BrunoKedzierski\\TaskUni\\TaskUni\\dane.csv");
            await studentDAO.LoadStudentData();

            try
            {
                studentDAO.DeleteStudentById(index);
            }
            catch (StudentNotFoundException ex)
            {
                return BadRequest(ex.Message);
            
            }


            return Ok();
        }




        [HttpPut("{index}")]
        public async Task<IActionResult> PutStudent([FromBody] Student student, [FromRoute] string index)
        {
            StudentDAO StudentDAO = new StudentDAO("E:\\Desktop\\cwiczenia3_jd-BrunoKedzierski\\TaskUni\\TaskUni\\dane.csv");

            await StudentDAO.LoadStudentData();

            student.NumerIndeksu = index;
            Student st = null;

            try
            {
                st = await StudentDAO.UpdateStudent(student);
            }
            catch ( StudentNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
           
            return Ok(st);
        }


    }



}
