using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskUni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        static List<String> list = new List<string>();





        [HttpGet]
        public IEnumerable<Student> GetStudentList()
        {
        }

        [HttpGet("{IndexNum}")]
        public Student GetStudentByIndex(int IndexNum) {


            return IndexNum;
        
        
        }

    }


}
