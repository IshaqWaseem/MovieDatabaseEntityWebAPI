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
using MovieDatabaseEntityWebAPI.Models.DTO.Franchise;
using MovieDatabaseEntityWebAPI.Models.Entities;
using MovieDatabaseEntityWebAPI.Services.Franchises;

namespace MovieDatabaseEntityWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FranchiseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFranchiseService _franchiseService;

        public FranchiseController(IFranchiseService franchiseService, IMapper mapper)
        {
            _mapper = mapper;
            _franchiseService = franchiseService;
        }



        // GET: api/Franchise
        /// <summary>
        /// Get all id, including its movies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetFranchises()
        {
            return Ok(
               _mapper.Map<List<FranchiseDto>>(await _franchiseService.GetAllAsync()));
        }

        // GET: api/Franchise/5
        /// <summary>
        /// get a franchise by id, including movies
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Franchise>> GetFranchise(int id)
        {
            try
            {
                return Ok(_mapper.Map<FranchiseDto>(
                        await _franchiseService.GetByIdAsync(id))
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

        // PUT: api/Franchise/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update a franchise by id
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchiseAsync(int id, FranchisePutDto franchise)
        {
            if (id != franchise.Id)
                return BadRequest();

            try
            {
                await _franchiseService.UpdateAsync(
                        _mapper.Map<Franchise>(franchise)
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

        // POST: api/Franchise
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// create a franchise
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostFranchiseAsync(FranchisePostDto franchiseDto)
        {
            Franchise franchise = _mapper.Map<Franchise>(franchiseDto);
            await _franchiseService.AddAsync(franchise);
            return CreatedAtAction("GetFranchise", new { id = franchise.Id }, franchise);
        }

        // DELETE: api/Franchise/5
        /// <summary>
        /// delete a franchise
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchiseAsync(int id)
        {
            try
            {
                await _franchiseService.DeleteByIdAsync(id);
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
        /// adds movies to franchise
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> UpdateFranchiseMoviesAsync(int[] movieIds, int id)
        {
            try
            {
                await _franchiseService.UpdateMoviesAsync(movieIds, id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
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
