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
    public class CampusController : ApiController
    {
        private OpendeurdagAPIContext db = new OpendeurdagAPIContext();

        // GET: api/Campus
        public IQueryable<Campus> GetCampus()
        {
            return db.Campus;
        }

        // GET: api/Campus/5
        [ResponseType(typeof(Campus))]
        public async Task<IHttpActionResult> GetCampus(int id)
        {
            Campus campus = await db.Campus.FindAsync(id);
            if (campus == null)
            {
                return NotFound();
            }

            return Ok(campus);
        }
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CampusExists(int id)
        {
            return db.Campus.Count(e => e.CampusId == id) > 0;
        }
    }
}