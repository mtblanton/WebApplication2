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
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ActivationPendingsController : ApiController
    {
        private Account_ServiceEntities db = new Account_ServiceEntities();

        // GET: api/ActivationPendings
        public IQueryable<ActivationPending> GetActivationPendings()
        {
            return db.ActivationPendings;
        }

        // GET: api/ActivationPendings/5
        [ResponseType(typeof(ActivationPending))]
        public IHttpActionResult GetActivationPending(string id)
        {
            ActivationPending activationPending = db.ActivationPendings.Find(id);
            if (activationPending == null)
            {
                return NotFound();
            }

            return Ok(activationPending);
        }

        // PUT: api/ActivationPendings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutActivationPending(string id, ActivationPending activationPending)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activationPending.ActivationCode)
            {
                return BadRequest();
            }

            db.Entry(activationPending).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivationPendingExists(id))
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

        // POST: api/ActivationPendings
        [ResponseType(typeof(ActivationPending))]
        public IHttpActionResult PostActivationPending(ActivationPending activationPending)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActivationPendings.Add(activationPending);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ActivationPendingExists(activationPending.ActivationCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = activationPending.ActivationCode }, activationPending);
        }

        // DELETE: api/ActivationPendings/5
        [ResponseType(typeof(ActivationPending))]
        public IHttpActionResult DeleteActivationPending(string id)
        {
            ActivationPending activationPending = db.ActivationPendings.Find(id);
            if (activationPending == null)
            {
                return NotFound();
            }

            db.ActivationPendings.Remove(activationPending);
            db.SaveChanges();

            return Ok(activationPending);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivationPendingExists(string id)
        {
            return db.ActivationPendings.Count(e => e.ActivationCode == id) > 0;
        }
    }
}