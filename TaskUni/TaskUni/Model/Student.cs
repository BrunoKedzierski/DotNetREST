namespace TaskUni.Model
{
    public class Student
    {


        public string Name { get; set; }
        public string Surname { get; set; }
        public string NumerIndeksu { get; set; }
        public string DataUrodzenia { get; set; }
        public string Studia { get; set; }
        public string Tryb { get; set; }
        public string Email { get; set; }
        public string ImieOjca { get; set; }
        public string ImieMatki { get; set; }

        public Student(string name, string surname, string numerIndeksu, string dataUrodzenia, string studia, string tryb, string email, string imieOjca, string imieMatki)
        {
            Name = name;
            Surname = surname;
            NumerIndeksu = numerIndeksu;
            DataUrodzenia = dataUrodzenia;
            Studia = studia;
            Tryb = tryb;
            Email = email;
            ImieOjca = imieOjca;
            ImieMatki = imieMatki;
        }

        public Student()
        {
        }
    }


}
