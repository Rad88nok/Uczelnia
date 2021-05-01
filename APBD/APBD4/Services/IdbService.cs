using APBD4.Controllers;
using APBD4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace APBD4.Services
{
    public interface IdbService
    {
        public IEnumerable<Animal> GetAnimals();
        public IEnumerable<Animal> GetAnimals(string OrderBy);
        public int AddAnimals(Animal animal);
        public int UpdateAnimal(Animal animal, string id);
        public int DeleteAnimal(string id);
    }

}
