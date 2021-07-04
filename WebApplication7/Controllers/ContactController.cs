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
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class ContactController : ApiController
    {
        private ContactEntities db = new ContactEntities();

        // GET: api/Contact
        public IQueryable<Table_1> GetTable_1()
        {
            return db.Table_1;
        }

        // GET: api/Contact/5
        [ResponseType(typeof(Table_1))]
        public IHttpActionResult GetTable_1(int id)
        {
            Table_1 table_1 = db.Table_1.Find(id);
            if (table_1 == null)
            {
                return NotFound();
            }

            return Ok(table_1);
        }

        // PUT: api/Contact/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTable_1(int id, Table_1 table_1)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            if (id != table_1.Id)
            {
                return BadRequest();
            }

            db.Entry(table_1).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Table_1Exists(id))
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

        // POST: api/Contact
        [ResponseType(typeof(Table_1))]
        public IHttpActionResult PostTable_1(Table_1 table_1)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            db.Table_1.Add(table_1);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = table_1.Id }, table_1);
        }

        // DELETE: api/Contact/5
        [ResponseType(typeof(Table_1))]
        public IHttpActionResult DeleteTable_1(int id)
        {
            Table_1 table_1 = db.Table_1.Find(id);
            if (table_1 == null)
            {
                return NotFound();
            }

            db.Table_1.Remove(table_1);
            db.SaveChanges();

            return Ok(table_1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Table_1Exists(int id)
        {
            return db.Table_1.Count(e => e.Id == id) > 0;
        }
    }
}