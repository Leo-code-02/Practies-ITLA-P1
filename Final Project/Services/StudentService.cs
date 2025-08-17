using Cafeteria_Final_Project_C_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cafeteria_Final_Project_C_.Services
{
    internal class StudentService
    {
        public StudentService() { }
        public List<Student> GetStudents()
        {
            using (var db = new Data.DBconection())
            {
                return db.Students.ToList();
            }
        }
        public Student GetStudentById(int id)
        {
            using (var db = new Data.DBconection())
            {
                return db.Students.FirstOrDefault(s => s.Id == id);
            }
        }
        public void AddStudent(Student student)
        {
            using (var db = new Data.DBconection())
            {
                db.Students.Add(student);
                db.SaveChanges();
            }
        }
        public void UpdateStudent(Student student)
        {
            using (var db = new Data.DBconection())
            {
                var existingStudent = db.Students.FirstOrDefault(s => s.Id == student.Id);
                if (existingStudent != null)
                {
                    existingStudent.FullName = student.FullName;
                    existingStudent.StudentNumber = student.StudentNumber;
                    existingStudent.Credit = student.Credit;
                    db.SaveChanges();
                }
            }
        }
      
        public void AddCredit(int studentId, decimal amount)
        {
            using (var db = new Data.DBconection())
            {
                var student = db.Students.FirstOrDefault(s => s.Id == studentId);
                if (student != null)
                {
                    student.Credit += amount;
                    db.SaveChanges();
                }
            }
        }
        public void DeductCredit(int studentId, decimal amount)
        {
            using (var db = new Data.DBconection())
            {
                var student = db.Students.FirstOrDefault(s => s.Id == studentId);
                if (student != null && student.Credit >= amount)
                {
                    student.Credit -= amount;
                    db.SaveChanges();
                }
            }
        }
        public List<Student> SearchStudents(string searchTerm)
        {
            using (var db = new Data.DBconection())
            {
                return db.Students
                    .Where(s => s.FullName.Contains(searchTerm) || s.StudentNumber.Contains(searchTerm))
                    .ToList();
            }
        }
        public List<Student> GetStudentsWithLowCredit(decimal threshold)
        {
            using (var db = new Data.DBconection())
            {
                return db.Students
                    .Where(s => s.Credit < threshold)
                    .ToList();
            }
        }
        public List<Student> GetStudentsWithHighCredit(decimal threshold)
        {
            using (var db = new Data.DBconection())
            {
                return db.Students
                    .Where(s => s.Credit >= threshold)
                    .ToList();
            }
        }
    }
}
