using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseEntityWebAPI.Exceptions;
using MovieDatabaseEntityWebAPI.Models;
using MovieDatabaseEntityWebAPI.Models.DTO.Movie;
using MovieDatabaseEntityWebAPI.Models.Entities;
using MovieDatabaseEntityWebAPI.Services.Movies;

namespace MovieDatabaseEntityWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _mapper = mapper;
            _movieService = movieService;
        }


        //GET: api/Movie
        /// <summary>
        /// Get all the movies in the database, including list of characters ids
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            return Ok(
                _mapper.Map<List<MovieDto>>(await _movieService.GetAllAsync()));
        }

        // GET: api/Movie/5
        /// <summary>
        /// Get movie by id, includes list of character ids
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            try
            {
                return Ok(_mapper.Map<MovieDto>(
                        await _movieService.GetByIdAsync(id))
                    );
            }
            catch (EntityNotFoundException ex)
            {
                // Formatting an error code for the exception messages.
                // Using the built in Problem Details.
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }
        }

        // PUT: api/Movie/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update movie by id, "ReleaseYear" CANNOT BE MORE THAN 4 CHARACTERS
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieAsync(int id, MoviePutDto movie)
        {
            if (id != movie.Id)
                return BadRequest();

            try
            {
                await _movieService.UpdateAsync(
                        _mapper.Map<Movie>(movie)
                    );
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                // Formatting an error code for the exception messages.
                // Using the built in Problem Details.
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }
        }

        // POST: api/Movie
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create a movie in the database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostMovieAsync(MoviePostDto movieDto)
        {
            Movie movie = _mapper.Map<Movie>(movieDto);
            await _movieService.AddAsync(movie);
            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movie/5
        /// <summary>
        /// Delete a movie in the database by Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            try
            {
                await _movieService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                // Formatting an error code for the exception messages.
                // Using the built in Problem Details.
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }
        }
        /// <summary>
        /// adds characters to movie
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}/characters")]
        public async Task<IActionResult> UpdateMovieCharactersAsync(int[] characterIds, int id)
        {
            try
            {
                await _movieService.UpdateCharactersAsync(characterIds, id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                // Formatting an error code for the exception messages.
                // Using the built in Problem Details.
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }
        }


    }
}
