using System.Linq;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ErrorsController : ApiController
    {
        private readonly Account_ServiceEntities db = new Account_ServiceEntities();

        // GET: api/Errors
        public IQueryable<Error> GetErrors()
        {
            return db.Errors;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ErrorExists(string id)
        {
            return db.Errors.Count(e => e.Application == id) > 0;
        }
    }
}