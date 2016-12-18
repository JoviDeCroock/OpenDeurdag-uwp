using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RondleidingAPI.Models;
using RondleidingAPI.Models.Domain;

namespace RondleidingAPI.Controllers
{
    public class StudentsController : ApiController
    {
        private RondleidingAPIContext db = new RondleidingAPIContext();

        // GET: api/Students
        public IQueryable<Student> GetStudents()
        {
            return db.Student;
        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(int id)
        {
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(int id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.StudentId)
            {
                return BadRequest();
            }

            /*TRYOUT*/
            Student s = db.Student.Where(t => t.StudentId == student.StudentId).FirstOrDefault();          
            if(student.PrefTraining.Count != s.PrefTraining.Count)
            {
                foreach(StudentTraining p in student.PrefTraining)
                {
                    if(!s.PrefTraining.Contains(p))
                    {
                        db.STraining.Add(p);
                        s.addTraining(p);
                    }
                }
            }
            if (student.PrefCampus.Count != s.PrefCampus.Count)
            {
                foreach (StudentCampus p in student.PrefCampus)
                {
                    if (!s.PrefCampus.Contains(p))
                    {
                        db.SCampus.Add(p);
                        s.addCampus(p);
                    }
                }
            }

            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Students
        [ResponseType(typeof(Student))]
        public IHttpActionResult PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Student.Add(student);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = student.StudentId }, student);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(int id)
        {
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.Student.Remove(student);
            db.SaveChanges();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.Student.Count(e => e.StudentId == id) > 0;
        }
    }
}