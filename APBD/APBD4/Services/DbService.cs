using APBD4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace APBD4.Services
{
    public class DbService : IdbService
    {
        private IConfiguration _configuration;
        public DbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static List<Animal> animals = new List<Animal>();
        public IEnumerable<Animal> GetAnimals()
        {
            using (var connect = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            using (var command = new SqlCommand())
            {
                command.Connection = connect;
                command.CommandText = "Select * From Animal";
                connect.Open();

                var read = command.ExecuteReader();
                while (read.Read())
                {
                    Animal animal = new Animal();

                    animal.Name = read["Name"].ToString();
                    animal.Description = read["Description"].ToString();
                    animal.Category = read["Category"].ToString();
                    animal.Area = read["Area"].ToString();
                    animal.IdAnimal = int.Parse(read["IdAnimal"].ToString());

                    animals.Add(animal);
                }
                connect.Close();
            }
            return animals;
            //throw new NotImplementedException();
        }
        public IEnumerable<Animal> GetAnimals(string orderBy)
        {
            using (var connect = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            using (var command = new SqlCommand())
            {
                command.Connection = connect;
                //if (orderBy == "name")
                //{
                //    command.CommandText = "Select * From Animal Order By @name";
                //    command.Parameters.AddWithValue("@name", orderBy);
                //}
                //if (orderBy == "description")
                //{
                //    command.CommandText = "Select * From Animal Order By @description";
                //    command.Parameters.AddWithValue("@description", orderBy);
                //}
                //if (orderBy == "category")
                //{
                //    command.CommandText = "Select * From Animal Order By @category";
                //    command.Parameters.AddWithValue("@category", orderBy);
                //}
                //if (orderBy == "area")
                //{
                //    command.CommandText = "Select * From Animal Order By @area";
                //    command.Parameters.AddWithValue("@area", orderBy);
                //}
                command.CommandText = "Select * From Animal Order By {@orderBy}";
                command.Parameters.AddWithValue("@orderBy", orderBy);
                connect.Open();

                var read = command.ExecuteReader();
                while (read.Read())
                {
                    Animal animal = new Animal();

                    animal.Name = read["Name"].ToString();
                    animal.Description = read["Description"].ToString();
                    animal.Category = read["Category"].ToString();
                    animal.Area = read["Area"].ToString();
                    animal.IdAnimal = int.Parse(read["IdAnimal"].ToString());

                    animals.Add(animal);
                }
                connect.Close();
            }
            return animals;
            //throw new NotImplementedException();
        }
        //public bool AddAnimals(JsonDocument json)//jeśli nie serializuję ręcznie to wyskakuje wciąż błąd że aplikacja nie przyjmuje obiektu json
        //{
        //    string jj = json.RootElement.GetProperty("name").GetString();

        //    Animal student = new Animal
        //    {
        //        IdAnimal = int.Parse(json.RootElement.GetProperty("studentId").GetString()),
        //        Name = json.RootElement.GetProperty("name").GetString(),
        //        Description = json.RootElement.GetProperty("description").GetString(),
        //        Category = json.RootElement.GetProperty("category").GetString(),
        //        Area = json.RootElement.GetProperty("area").GetString()
        //    };
        //    //throw new NotImplementedException();
        //    if (student != null)
        //    {
        //        animals.Add(student);
        //        writer.WriteLine(student.ToString());
        //        return true;
        //    }
        //    else return false;
        //}
        public int AddAnimals(Animal animal)
        {
            using (var connect = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            using (var command = new SqlCommand())
            {
                command.CommandText = "Insert INTO Animal" + "Valuses(@IdAnimal, @Name, @Description, @Category, @Area";

                command.Parameters.AddWithValue("IdAnimal", animal.IdAnimal);
                command.Parameters.AddWithValue("Name", animal.Name);
                command.Parameters.AddWithValue("Description", animal.Description);
                command.Parameters.AddWithValue("Category", animal.Category);
                command.Parameters.AddWithValue("Area", animal.Area);
                connect.Open();

                return command.ExecuteNonQuery();
            }
        }
        //public bool UpdateAnimal(JsonDocument json)
        //{
        //    string tmpAnimalId = json.RootElement.GetProperty("studentId").GetString();
        //    bool result = false;
        //    foreach (Animal s in animals)
        //    {
        //        if (s.IdAnimal == int.Parse(tmpAnimalId))
        //        {
        //            if (json.RootElement.GetProperty("name").GetString() != null)
        //            {
        //                s.Name = json.RootElement.GetProperty("name").GetString();
        //                if (json.RootElement.GetProperty("description").GetString() != null)
        //                {
        //                    s.Description = json.RootElement.GetProperty("description").GetString();
        //                    if (json.RootElement.GetProperty("category").GetString() != null)
        //                    {
        //                        s.Category = json.RootElement.GetProperty("category").GetString();
        //                        if (json.RootElement.GetProperty("area").GetString() != null)
        //                        {
        //                            s.Area = json.RootElement.GetProperty("area").GetString();

        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return result;
        //}
        public int UpdateAnimal(Animal animal, string id)
        {
            using (var connect = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            using (var command = new SqlCommand())
            {
                command.CommandText = "Update Animal" + "Valuses(@IdAnimal, @Name, @Description, @Category, @Area";
                command.Parameters.AddWithValue("IdAnimal", animal.IdAnimal);
                command.Parameters.AddWithValue("Name", animal.Name);
                command.Parameters.AddWithValue("Description", animal.Description);
                command.Parameters.AddWithValue("Category", animal.Category);
                command.Parameters.AddWithValue("Area", animal.Area);
                connect.Open();

                return command.ExecuteNonQuery();
            }
        }
        public int DeleteAnimal(string id)
        {
            using (var connect = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            using (var command = new SqlCommand())
            {
                command.CommandText = "Delate From Animal Where IdAnimal = @IdAnimal";
                command.Parameters.AddWithValue("IdAnimal", id);
                connect.Open();

                return command.ExecuteNonQuery();
            }
            //throw new NotImplementedException();
        }
    }
}
