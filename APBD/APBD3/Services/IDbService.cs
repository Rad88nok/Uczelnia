using APBD3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace APBD3.Services
{
    public interface IDbService
    {
        public bool AddStudent(JsonDocument json);
        public Student GetStudent(string id);
        public List<Student> GetStudents(string orderBy);
        public bool UpdateStudent(JsonDocument student);
        public void DeleteStudent(string id);
        
    }
}
