using Animals.DTOs;
using System.Data.SqlClient;

namespace Animals.Services
{
    public class MssqlService : IDatabaseService
    {

        IConfiguration _configuration;

        public MssqlService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

     
        public SqlConnection PrepareConnection() {

            return new SqlConnection(_configuration.GetConnectionString("ProdDb"));
        
        }

        public IEnumerable<Animal> ShowAllAnimals(string orderParam)
        {
            List<Animal> animals = new List<Animal>();
            using SqlConnection conn = PrepareConnection();

            SqlCommand comm = new SqlCommand( "SELECT * FROM Animal", conn);

            conn.Open();

            var reader = comm.ExecuteReader();
            while(reader.Read())
            {
                animals.Add(new Animal {
                IdAnimal = (int)reader["IdAnimal"],
                Name = reader["name"].ToString(),
                Category = reader["category"].ToString(),
                Description = reader["description"].ToString(),
                Area   = reader["area"].ToString()
                });



            }


            if (new Animal().GetType().GetProperty(orderParam) == null)
                throw new ArgumentException($"order parameter not found: {orderParam}");
            
            return animals.OrderBy(a => a.GetType().GetProperty(orderParam).GetValue(a));
        }


        public void AddNewAnimal(Animal newAnimal)
        {
            using SqlConnection con = PrepareConnection();
            SqlCommand comm = new SqlCommand("Insert into Animal VALUES (@name, @description, @category, @area)", con);
            comm.Parameters.AddWithValue("@name", newAnimal.Name);
            comm.Parameters.AddWithValue("@description", newAnimal.Description);
            comm.Parameters.AddWithValue("@category", newAnimal.Category);
            comm.Parameters.AddWithValue("@area", newAnimal.Area);
            con.Open();
            comm.ExecuteNonQuery();
        }

        public void DeleteAnimal(int idAnimal)
        {
            using SqlConnection con = PrepareConnection();
            SqlCommand command = new SqlCommand("DELETE FROM Animal WHERE IdAnimal = @idAnimal;", con);
            command.Parameters.AddWithValue("@idAnimal", idAnimal);
            con.Open();
            var rowsAfffected = command.ExecuteNonQuery();
            if (rowsAfffected == 0) {
                throw new ArgumentException($"No such id: {idAnimal}");
            }
        }

        public void UpdateAnimal(Animal newAnimal, int idToChange)
        {
            using SqlConnection con  = PrepareConnection();
            SqlCommand comm = new SqlCommand("UPDATE Animal SET name = @name, description = @description, category = @category, area = @area WHERE IdAnimal = @idToChange; ", con);
            comm.Parameters.AddWithValue("@name", newAnimal.Name);
            comm.Parameters.AddWithValue("@description", newAnimal.Description);
            comm.Parameters.AddWithValue("@category", newAnimal.Category);
            comm.Parameters.AddWithValue("@area", newAnimal.Area);
            comm.Parameters.AddWithValue("@IdAnimal", idToChange);
            var rowsAfffected = comm.ExecuteNonQuery();
            if (rowsAfffected == 0)
            {
                throw new ArgumentException($"No such id: {idToChange}");
            }
        }
    }
}
