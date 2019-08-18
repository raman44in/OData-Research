using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using WebAppCoreEF2.Models;

namespace WebAppCoreEF2.Controllers
{
   // [Route("api/[controller]")]
    //[ApiController]
    public class ValuesController : ODataController
    {
        public const int pageSize = 10;

        private SchoolExtendedContext _schoolContext;
        private SchoolContext _context;
        public ValuesController(SchoolExtendedContext schoolContext,SchoolContext context) {
            _schoolContext = schoolContext;
            _context = context;
        }

        [HttpGet]
        [ODataRoute("Course")]
        [EnableQuery]

        public IActionResult Course() {

            return Ok(_context.Course);
                }

        [HttpGet]
        [ODataRoute("Person")]
        [EnableQuery(PageSize = 5)]
        public IActionResult View1()
        {
            IQueryable<Vw_ExtendedPerson> vw_ExtendedPerson = null;

            vw_ExtendedPerson = (from sc in _context.vw_ExtendedPeople
                                 join s in _context.Person on sc.PersonId equals s.PersonId
                                 select new Vw_ExtendedPerson()
                                 {
                                     FirstName = s.FirstName,
                                     LastName = s.LastName,
                                     Gender = sc.Gender,
                                     Married = sc.Married,
                                     PersonId = sc.PersonId,
                                     Race = sc.Race

                                 });


            return Ok(vw_ExtendedPerson);

            //var query = _peopleService.GetAllAsQueryable(); //Abstracted from the implementation of db access. Just returns IQueryable<People>
            // var queryResults = (IQueryable<Person>)queryOptions.ApplyTo(_context.Person);
            //  return Ok(new CustomResult<Person> { Items = queryResults, TotalCount = queryResults.Count() });


            //var queryResults = (IQueryable<Person>)queryOptions.ApplyTo(_context.Person);
            //  return Ok(new CustomResult<Person> { Items = queryResults  , TotalCount = queryResults.Count()});

        }

        //[HttpGet]
        ////[EnableQuery(PageSize = pageSize)]
        //[ODataRoute("vw_ExtendedPerson")]
        //public PageResult<Vw_ExtendedPerson> vw_ExtendedPerson(ODataQueryOptions<Vw_ExtendedPerson> queryOptions)
        //{
        //    IQueryable<Vw_ExtendedPerson> vw_ExtendedPerson = null;

        //    vw_ExtendedPerson = (from sc in _context.vw_ExtendedPeople
        //                         join s in _context.Person on sc.PersonId equals s.PersonId
        //                         select new Vw_ExtendedPerson()
        //                         {
        //                             FirstName = s.FirstName,
        //                             LastName = s.LastName,
        //                             Gender = sc.Gender,
        //                             Married = sc.Married,
        //                             PersonId = sc.PersonId,
        //                             Race = sc.Race

        //                         });
         
        //    var queryResults = (IQueryable<Vw_ExtendedPerson>)queryOptions.ApplyTo(vw_ExtendedPerson);



        //    var isCountable = queryOptions.Count == null ? "false" : "true";
        //    var skipCount = queryOptions.Skip?.Value;
        //    var takeCount = queryOptions.Top?.Value;
        //    //var x = $"odata/person?$count={isCountable}&$skip={skipCount??pageSize}&$take={pageSize-takeCount}";
        //    return new PageResult<Vw_ExtendedPerson>(queryResults, new Uri("odata/vw_ExtendedPerson", UriKind.Relative) , null);
        //}



        [HttpGet]
        [Route("custom")]
        public IActionResult custom()
        {
            return Ok("gehehe");
        }
    }
    public class CustomResult<T>
    {
        public long? TotalCount { get; set; }
        public IQueryable<T> Items { get; set; }
    }
}
