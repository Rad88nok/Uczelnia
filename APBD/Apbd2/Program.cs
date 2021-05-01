using Apbd2.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Apbd2
{
    class Program
    {
        static void Main(string[] args)
        {
            LogException logE = LogException.GetInstance();
            if (String.IsNullOrEmpty(args[0]))
            {
                logE.Error(new Exception("ArgumentException:The first argument is missing(in) "));
                throw new ArgumentException("Podana ścieżka jest niepoprawna(Dane wejściowe)");
            }
            if (!File.Exists(args[0]))
            {
                logE.Error(new Exception("FileNotFoundException:The specified file does not exist "));
                throw new FileNotFoundException("Plik " + args[0] + " nie iestnieje");
            }
            if (String.IsNullOrEmpty(args[1]))
            {
                logE.Error(new Exception("ArgumentException:The first argument is missing(out) "));
                throw new ArgumentException("Podana ścieżka jest niepoprawna(Dane wyjściowe)");
            }
            var path = args[0];
            var fi = new FileInfo(path);


            HashSet<Student> fileContent = new HashSet<Student>(new OwnComparer());
            var dateAndTime = DateTime.Now;
            var date = dateAndTime.ToShortDateString();

            using (var stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] columns = line.Split(",");
                    bool allData = true;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(columns[i]))
                        {
                            allData = false;
                        }
                    }
                    Student student = new Student();
                    try
                    {
                        if (columns.Length == 9 && allData == true)
                        {
                            student.name = columns[0];
                            student.surname = columns[1];
                            student.study = new Study(columns[2], columns[3]);
                            student.studentId = "s" + columns[4];
                            student.birthDate = DateTime.Parse(columns[5]);
                            student.email = columns[6];
                            student.mothersName = columns[7];
                            student.fathersName = columns[8];
                            if (!fileContent.Contains(student))
                            {
                                fileContent.Add(student);
                            }
                            else
                            {
                                logE.DuplicatedStudentDataException(line);
                            }
                        }
                        else
                        {
                            logE.NotEnoughStudentDataException(line);
                        }
                    }
                    catch (Exception e)
                    {
                        Exception eMessage = new Exception(e.Message + " " + line);
                        logE.Error(eMessage);
                    }
                }
            }
            var university = new University();
            university.date = date;
            university.author = "Kazimierczyk Konrad";
            university.students = fileContent;
            var tj = new toJson();
            tj.university = university;
            var json = JsonSerializer.SerializeToUtf8Bytes(tj);
            //File.WriteAllText("log.json", json, Encoding.UTF8);
            File.WriteAllBytes(args[1] + "/data.json", json);
            //WebClient Encoding = Encoding.UTF8;


        }

    }
}
