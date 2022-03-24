using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TaskUni.Exceptions;

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
                    Student st = ParseFromCsv(line);
                    if (st != null)
                    {
                        _studentsData.Add(ParseFromCsv(line));
                    }
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


            int index = _studentsData.FindIndex(s => s.NumerIndeksu.Equals(id));

            if (index == -1)
                throw new StudentNotFoundException($"Could not find student with id of: {id}");

            return _studentsData[index];
        
        
        }

        public void AddStudent(Student st) {
            string Id = st.NumerIndeksu;

            if (_studentsData.FindIndex((s) => s.NumerIndeksu.Equals(Id)) != -1)
                throw new DuplicatedStudentIdException($"Student with Id of {Id} already exists");

            _studentsData.Add(st);
        }

        public void DeleteStudentById(string Id) {
            int index = _studentsData.FindIndex(st => st.NumerIndeksu.Equals(Id));

            if (index == -1)
                throw new StudentNotFoundException($"Could not find student with id of: {Id}");

            _studentsData.RemoveAt(index);
       
        }

        public Student UpdateStudent(Student student) {

            string Id = student.NumerIndeksu;

            int index = _studentsData.FindIndex(st => st.NumerIndeksu.Equals(Id));

            if(index == -1)
                throw new StudentNotFoundException($"Could not find student with id of: {Id}");

            _studentsData[index] = student;

            return student;

        }

    }
}
