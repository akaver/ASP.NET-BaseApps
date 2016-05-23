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
using DAL;
using Domain;

namespace WebApi.Server.Controllers.Api
{
    public class TranslationsController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();

        // GET: api/Translations
        public IQueryable<Translation> GetTranslations()
        {
            return db.Translations;
        }

        // GET: api/Translations/5
        [ResponseType(typeof(Translation))]
        public IHttpActionResult GetTranslation(int id)
        {
            Translation translation = db.Translations.Find(id);
            if (translation == null)
            {
                return NotFound();
            }

            return Ok(translation);
        }

        // PUT: api/Translations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTranslation(int id, Translation translation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != translation.TranslationId)
            {
                return BadRequest();
            }

            db.Entry(translation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranslationExists(id))
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

        // POST: api/Translations
        [ResponseType(typeof(Translation))]
        public IHttpActionResult PostTranslation(Translation translation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Translations.Add(translation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = translation.TranslationId }, translation);
        }

        // DELETE: api/Translations/5
        [ResponseType(typeof(Translation))]
        public IHttpActionResult DeleteTranslation(int id)
        {
            Translation translation = db.Translations.Find(id);
            if (translation == null)
            {
                return NotFound();
            }

            db.Translations.Remove(translation);
            db.SaveChanges();

            return Ok(translation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TranslationExists(int id)
        {
            return db.Translations.Count(e => e.TranslationId == id) > 0;
        }
    }
}