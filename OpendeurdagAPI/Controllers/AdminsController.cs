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
using System.Text;
using System.Security.Cryptography;

namespace OpendeurdagAPI.Controllers
{
    public class AdminsController : ApiController
    {
        private string encode(string pw)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(pw));
            return Convert.ToBase64String(hashedDataBytes);
        }

        private OpendeurdagAPIContext db = new OpendeurdagAPIContext();

        // GET: api/Admins
        public IQueryable<Admin> GetAdmins()
        {
            return db.Admins;
        }

        // GET: api/Admins/5
        [ResponseType(typeof(Admin))]
        public async Task<IHttpActionResult> GetAdmin(int id)
        {
            Admin admin = await db.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }

        // POST: api/Admins
        [ResponseType(typeof(Admin))]
        public async Task<IHttpActionResult> PostAdmin(Admin admin)
        {
            if(admin.Email!= null || admin.Password!=null)
            {
                return Ok("Vul alle velden in");
            }
            Admin a = db.Admins.FirstOrDefault(ad => ad.Email == admin.Email);
            if (a == null)
            {
                return NotFound();
            }

            string x = encode(admin.Password);
            if (a.Password == x)
            {
                return Ok(a);
            }
            return Ok("Foutief wachtwoord.");
        }

        // DELETE: api/Admins/5
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdminExists(int id)
        {
            return db.Admins.Count(e => e.AdminId == id) > 0;
        }
    }
}