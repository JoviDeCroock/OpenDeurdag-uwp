using System;
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
    public class TrainingsController : ApiController
    {
        private OpendeurdagAPIContext db = new OpendeurdagAPIContext();

        // GET: api/Trainings
        public IQueryable<Training> GetTrainings()
        {
            return db.Trainings;
        }

        // GET: api/Trainings/5
        [ResponseType(typeof(Training))]
        public async Task<IHttpActionResult> GetTraining(int id)
        {
            Training training = await db.Trainings.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }

            return Ok(training);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrainingExists(int id)
        {
            return db.Trainings.Count(e => e.TrainingId == id) > 0;
        }
    }
}