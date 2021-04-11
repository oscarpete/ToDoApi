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
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    public class toDoItemsController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/toDoItems
        public IQueryable<toDoItems> GettoDoItems()
        {
            return db.toDoItems;
        }

        // GET: api/toDoItems/5
        [ResponseType(typeof(toDoItems))]
        public IHttpActionResult GettoDoItems(int id)
        {
            toDoItems toDoItems = db.toDoItems.Find(id);
            if (toDoItems == null)
            {
                return NotFound();
            }

            return Ok(toDoItems);
        }

        // PUT: api/toDoItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttoDoItems(int id, toDoItems toDoItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toDoItems.Id)
            {
                return BadRequest();
            }

            db.Entry(toDoItems).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!toDoItemsExists(id))
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

        // POST: api/toDoItems
        [ResponseType(typeof(toDoItems))]
        public IHttpActionResult PosttoDoItems(toDoItems toDoItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.toDoItems.Add(toDoItems);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = toDoItems.Id }, toDoItems);
        }

        // DELETE: api/toDoItems/5
        [ResponseType(typeof(toDoItems))]
        public IHttpActionResult DeletetoDoItems(int id)
        {
            toDoItems toDoItems = db.toDoItems.Find(id);
            if (toDoItems == null)
            {
                return NotFound();
            }

            db.toDoItems.Remove(toDoItems);
            db.SaveChanges();

            return Ok(toDoItems);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool toDoItemsExists(int id)
        {
            return db.toDoItems.Count(e => e.Id == id) > 0;
        }
    }
}