using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TaskUni.Model
{
    public class StudentDAO
    {

        private List<Student> _studentsData;

        public string DataPath { get; set; }


        public StudentDAO(string dataPath)
        {
            DataPath = dataPath;
        }

        public StudentDAO()
        {
        }



        public async Task LoadStudentData() {

            FileInfo fi = new(DataPath);
            
            _studentsData = new List<Student>();   

            using (StreamReader stream = new(fi.OpenRead()))
            {
                string line = null;

                while ((line =  await stream.ReadLineAsync()) != null)
                {
                    _studentsData.Add(ParseFromCsv(line));
                }


            }
            

        }

        public Student ParseFromCsv(string CsvEntry) {


            string[] Attributes = CsvEntry.Split(',');

            if(Attributes.Length != 9)
                return null;



            return new Student
            {

                Name = Attributes[0],
                Surname = Attributes[1],
                NumerIndeksu = Attributes[2],
                DataUrodzenia = Attributes[3],
                Studia = Attributes[4],
                Tryb = Attributes[5],
                Email = Attributes[6],
                ImieMatki = Attributes[7],
                ImieOjca = Attributes[8],

            };
        
        
        }
        public IEnumerable<Student> GetAllStudents() {

            return _studentsData;

        }

        public Student GetStudentById(string id) {


            Student student = _studentsData.Find(s => s.NumerIndeksu.Equals(id));

            return student;
        
        
        }

    }
}
