using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private MoviesDbContext context;
        public MoviesController(MoviesDbContext context)
        {
            this.context = context;
        }

        // GET: api/Flowers
        [HttpGet]
        public IEnumerable<Movie> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to)
        {
            IQueryable<Movie> result = context.Movies.Include(m => m.Comments);
            if (from == null && to == null)
            {
                return result;
            }
            if (from != null)
            {
                result = result.Where(m => m.DateAdded >= from);
            }
            if (to != null)
            {
                result = result.Where(m => m.DateAdded <= to); 
            }
            result = result.AsQueryable().OrderBy(m => m.YearOfRelease);
            
            // result = result.Where(m => m.YearOfRelease.OrderByDescending());
            // result.orderby m.YearOfRelease descending;

            return result;
        }

        // GET: api/Movies
        //[HttpGet]
        //public IEnumerable<Movie> Get()
        //{
         //   return context.Movies;
        //}

        // GET: api/Movies/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var existing = context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            return Ok(existing);
        }


        // GET: api/Movies/???
        //[HttpGet("{d1}/{d2}", Name = "Get")]
        //public IEnumerable<Movie> Get(DateTime d1, DateTime d2)
        //{
        //    var filteredMovies = from m in context.Movies
        //                         where m.DateAdded >= d1 && m.DateAdded <= d2
        //                         orderby m.YearOfRelease descending
        //                         select m;

        //    return filteredMovies;
        //}


        // POST: api/Movies
        [HttpPost]
        public void Post([FromBody] Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie movie)
        {
            var existing = context.Movies.AsNoTracking().FirstOrDefault(m => m.Id == id);
            if (existing == null)
            {
                context.Movies.Add(movie);
                context.SaveChanges();
                return Ok(movie);
            }
            movie.Id = id;
            context.Movies.Update(movie);
            context.SaveChanges();
            return Ok(movie);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            context.Movies.Remove(existing);
            context.SaveChanges();
            return Ok();
        }
    }
}