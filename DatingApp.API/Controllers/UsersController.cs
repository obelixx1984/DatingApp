using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserAktywny))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParametry userParametry)
        {
            var dowolnyUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _repo.GetUser(dowolnyUserId);

            userParametry.UserId = dowolnyUserId;

            if (string.IsNullOrEmpty(userParametry.Plec))
            {
                userParametry.Plec = userFromRepo.Plec == "mezczyzna" ? "kobieta" : "mezczyzna";
            }

            var users = await _repo.GetUsers(userParametry);

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            Response.DodajPaginacje(users.DomyslnaStrona, users.RozmiarStrony,
                users.CaloscKont, users.CaloscStron);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserDetaleDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _repo.ZapiszWszystko())
                return NoContent();

            throw new Exception($"Zapisywanie użytkownika {id} nie powiodło się!");
        }

        [HttpPost("{id}/lubie/{recipientId}")]
        public async Task<IActionResult> LubiUser(int id, int recipientId)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var lubie = await _repo.GetLubie(id, recipientId);

            if (lubie != null)
                return BadRequest("Już lubisz tego użytkownika");

            if (await _repo.GetUser(recipientId) == null)
                return NotFound();

            lubie = new Lubie
            {
                LubiId = id,
                LubiiId = recipientId
            };

            _repo.Dodaj<Lubie>(lubie);

            if (await _repo.ZapiszWszystko())
                return Ok();

            return BadRequest("Nie udało polubić się użytkownika");
        }
    }
}