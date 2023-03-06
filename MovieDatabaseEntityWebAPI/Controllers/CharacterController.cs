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
using MovieDatabaseEntityWebAPI.Models.DTO.Character;
using MovieDatabaseEntityWebAPI.Models.Entities;
using MovieDatabaseEntityWebAPI.Services.Characters;

namespace MovieDatabaseEntityWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService,IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }


        // GET: api/Character
        /// <summary>
        /// Get all characters in database, including its movies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharacters()
        {
            return Ok(
                _mapper.Map<List<CharacterDto>>(await _characterService.GetAllAsync()));
        }

        // GET: api/Character/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
            try
            {
                return Ok(_mapper.Map<CharacterDto>(
                        await _characterService.GetByIdAsync(id))
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

        // PUT: api/Character/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// get a character by Id, includes movies
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacterAsync(int id, CharacterPutDto character)
        {
            if (id != character.Id)
                return BadRequest();

            try
            {
                await _characterService.UpdateAsync(
                        _mapper.Map<Character>(character)
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


        // POST: api/Character
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// create a character in the database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostCharacterAsync(CharacterPostDto characterDto)
        {
            Character character = _mapper.Map<Character>(characterDto);
            await _characterService.AddAsync(character);
            return CreatedAtAction("GetCharacter", new { id = character.Id }, character);

        }

        // DELETE: api/Character/5
        /// <summary>
        /// delete a character from database
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacterAsync(int id)
        {
            try
            {
                await _characterService.DeleteByIdAsync(id);
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
