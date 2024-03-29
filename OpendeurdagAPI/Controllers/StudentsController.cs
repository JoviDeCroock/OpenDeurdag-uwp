﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using OpendeurdagAPI.Models;
using OpendeurdagAPI.Models.Domain;

namespace OpendeurdagAPI.Controllers
{
    public class StudentsController : ApiController
    {
        private OpendeurdagAPIContext db = new OpendeurdagAPIContext();

        // GET: api/Students
        public IQueryable<Student> GetStudent()
        {
            return db.Student;
        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> GetStudent(int id)
        {
            Student student = await db.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }


        // POST: api/Students
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (student.Street == null || student.HouseNumber == null || student.Province == null || student.City == null || student.Name == null || student.Email == null)
            {
                return Ok("Vul alle velden in");
            }
            /*OPVULLEN MET ORIGINELE DB OBJECTEN OF HIJ MAAKT NIEUWE CAMPUSSEN EN TRAININGEN AAN*/
            List<Campus> dbC = new List<Campus>();
            List<Training> dbT = new List<Training>();
            foreach (Campus d in student.PrefCampus)
            {
                dbC.Add(db.Campus.Where(c => c.CampusId == d.CampusId).FirstOrDefault());
            }
            foreach (Training d in student.PrefTraining)
            {
                dbT.Add(db.Trainings.Where(c => c.TrainingId == d.TrainingId).FirstOrDefault());
            }
            student.PrefCampus = dbC;
            student.PrefTraining = dbT;

            db.Student.Add(student);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = student.StudentId }, student);
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