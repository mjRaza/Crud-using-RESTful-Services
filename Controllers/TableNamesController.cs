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
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class TableNamesController : ApiController
    {
        private MyDbEntities db = new MyDbEntities();

        // GET: api/TableNames
        public IQueryable<TableName> GetTableNames()
        {
            return db.TableNames;
        }

        // GET: api/TableNames/5
        [ResponseType(typeof(TableName))]
        public IHttpActionResult GetTableName(int id)
        {
            TableName tableName = db.TableNames.Find(id);
            if (tableName == null)
            {
                return NotFound();
            }

            return Ok(tableName);
        }

        // PUT: api/TableNames/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTableName(int id, TableName tableName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tableName.id)
            {
                return BadRequest();
            }

            db.Entry(tableName).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableNameExists(id))
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

        // POST: api/TableNames
        [ResponseType(typeof(TableName))]
        public IHttpActionResult PostTableName(TableName tableName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TableNames.Add(tableName);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tableName.id }, tableName);
        }

        // DELETE: api/TableNames/5
        [ResponseType(typeof(TableName))]
        public IHttpActionResult DeleteTableName(int id)
        {
            TableName tableName = db.TableNames.Find(id);
            if (tableName == null)
            {
                return NotFound();
            }

            db.TableNames.Remove(tableName);
            db.SaveChanges();

            return Ok(tableName);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TableNameExists(int id)
        {
            return db.TableNames.Count(e => e.id == id) > 0;
        }
    }
}