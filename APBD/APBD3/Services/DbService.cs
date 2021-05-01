using APBD3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;

namespace APBD3.Services
{
    public class DbService : IDbService
    {
        public static List<Student> students = new List<Student>();
        static DbService()
        {
            var fi = new FileInfo(".\\Data\\dataBase.csv");
            using (var stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] columns = line.Split(",");
                    Student student = new Student();
                    student.name = columns[0];
                    student.surname = columns[1];
                    student.studentId = columns[2];
                    student.birthDate = columns[3];
                    student.studyName = columns[4];
                    student.studySemestrNumber = columns[5];
                    student.email = columns[6];
                    student.mothersName = columns[7];
                    student.fathersName = columns[8];
                    students.Add(student);
                }
            }

        }
        public bool AddStudent(JsonDocument json)
        {
            string jj = json.RootElement.GetProperty("name").GetString();

            StreamWriter writer = new StreamWriter(".\\Data\\dataBase.csv", true);
            Student student = new Student
            {
                name = json.RootElement.GetProperty("name").GetString(),
                surname = json.RootElement.GetProperty("surname").GetString(),
                studentId = json.RootElement.GetProperty("studentId").GetString(),
                birthDate = json.RootElement.GetProperty("birthDate").GetString(),
                studyName = json.RootElement.GetProperty("studyName").GetString(),
                studySemestrNumber = json.RootElement.GetProperty("studySemestrNumber").GetString(),
                email = json.RootElement.GetProperty("email").GetString(),
                mothersName = json.RootElement.GetProperty("mothersName").GetString(),
                fathersName = json.RootElement.GetProperty("fathersName").GetString()
            };
            //throw new NotImplementedException();
            if (student != null)
            {
                students.Add(student);
                writer.WriteLine(student.ToString());
                return true;
            }
            else return false;
        }

        public void DeleteStudent(string id)
        {
            int index = 0;
            foreach (Student s in students)
            {
                if (s.studentId != id)
                    index++;
                else
                    break;
            }
            students.RemoveAt(index);
            //throw new NotImplementedException();
        }

        public Student GetStudent(string id)
        {
            foreach (Student s in students)
            {
                if (s.studentId == id) return s;
            }
            return null;
            //throw new NotImplementedException();

        }

        public List<Student> GetStudents(string orderBy)
        {
            //throw new NotImplementedException();
            if (orderBy == "name")
                return (List<Student>)students.OrderBy(Student => Student.name).ToList();
            else if (orderBy == "surname")
                return (List<Student>)students.OrderBy(Student => Student.surname).ToList();
            else
                return (List<Student>)students.OrderBy(Student => Student.studentId).ToList();
        }
        public bool UpdateStudent(JsonDocument json)
        {
            string tmpStudentId = json.RootElement.GetProperty("studentId").GetString();
            bool result = false;
            foreach (Student s in students)
            {
                if (s.studentId == tmpStudentId)
                {
                    if (json.RootElement.GetProperty("name").GetString() != null) 
                    { 
                        s.name = json.RootElement.GetProperty("name").GetString();
                        if (json.RootElement.GetProperty("surname").GetString() != null)
                        {
                            s.surname = json.RootElement.GetProperty("surname").GetString();
                            if (json.RootElement.GetProperty("birthDate").GetString() != null)
                            {
                                s.birthDate = json.RootElement.GetProperty("birthDate").GetString();
                                if (json.RootElement.GetProperty("studyName").GetString() != null)
                                {
                                    s.studyName = json.RootElement.GetProperty("studyName").GetString();
                                    if (json.RootElement.GetProperty("studySemestrNumber").GetString() != null)
                                    {
                                        s.studySemestrNumber = json.RootElement.GetProperty("studySemestrNumber").GetString();
                                        if (json.RootElement.GetProperty("email").GetString() != null)
                                        {
                                            s.email = json.RootElement.GetProperty("email").GetString();
                                            if (json.RootElement.GetProperty("mothersName").GetString() != null)
                                            {
                                                s.mothersName = json.RootElement.GetProperty("mothersName").GetString();
                                                if (json.RootElement.GetProperty("fathersName").GetString() != null)
                                                {
                                                    s.fathersName = json.RootElement.GetProperty("fathersName").GetString();
                                                    result = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }  
}
