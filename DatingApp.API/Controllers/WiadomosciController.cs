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
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class WiadomosciController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public WiadomosciController(IDatingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}", Name = "GetWiadomosci")]
        public async Task<IActionResult> GetWiadomosci(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var wiadomoscFromRepo = await _repo.GetWiadomosci(id);

            if (wiadomoscFromRepo == null)
                return NotFound();
            
            return Ok(wiadomoscFromRepo);
        }

        [HttpGet]
        public async Task<IActionResult> GetWiadomosciDoUser(int userId,
            [FromQuery]WiadomosciParametry wiadomosciParametry)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            wiadomosciParametry.UserId = userId;

            var wiadomoscFromRepo = await _repo.GetWiadomosciDoUser(wiadomosciParametry);

            var wiadomosci = _mapper.Map<IEnumerable<WiadomoscZwrocDto>>(wiadomoscFromRepo);

            Response.DodajPaginacje(wiadomoscFromRepo.DomyslnaStrona, wiadomoscFromRepo.RozmiarStrony, 
                wiadomoscFromRepo.CaloscKont,wiadomoscFromRepo.CaloscStron);

            return Ok(wiadomosci);
        }

        [HttpGet("watek/{odbiorcaId}")]
        public async Task<IActionResult> GetWiadomosciWatek(int userId, int odbiorcaId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var wiadomoscFromRepo = await _repo.GetWiadomosciWatek(userId, odbiorcaId);

            var wiadomoscWatek = _mapper.Map<IEnumerable<WiadomoscZwrocDto>>(wiadomoscFromRepo);

            return Ok(wiadomoscWatek);
        }

        [HttpPost]
        public async Task<IActionResult> UtworzWiadomosc(int userId, WiadomoscUtworzDto wiadomoscUtworzDto)
        {
            var wyslal = await _repo.GetUser(userId);
            
            if (wyslal.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            wiadomoscUtworzDto.WyslalId = userId;

            var odbiorca = await _repo.GetUser(wiadomoscUtworzDto.OdbiorcaId);

            if (odbiorca == null)
                return BadRequest("Nie można znaleźć użytkownika");

            var wiadomosci = _mapper.Map<Wiadomosci>(wiadomoscUtworzDto);

            _repo.Dodaj(wiadomosci);

            if (await _repo.ZapiszWszystko())
            {
                var utworzWiadomosc = _mapper.Map<WiadomoscZwrocDto>(wiadomosci);
                return CreatedAtRoute("GetWiadomosci",
                    new {userId, id = wiadomosci.Id}, utworzWiadomosc);
            }

             throw new Exception("Nie utworzono wiadomosci i nie zapisano");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UsunWiadomosc(int id, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var wiadomoscFromRepo = await _repo.GetWiadomosci(id);

            if (wiadomoscFromRepo.WyslalId == userId)
                wiadomoscFromRepo.WysylajacyUsunal = true;

            if (wiadomoscFromRepo.OdbiorcaId == userId)
                wiadomoscFromRepo.OdbiorcaUsunal= true;
            
            if (wiadomoscFromRepo.WysylajacyUsunal && wiadomoscFromRepo.OdbiorcaUsunal)
                _repo.Usun(wiadomoscFromRepo);

            if (await _repo.ZapiszWszystko())
                return NoContent();

            throw new Exception("Blad usunietej wiadomosci");
        }

        [HttpPost("{id}/przeczytana")]
        public async Task<IActionResult> WiadomoscPrzeczytana(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var wiadomosci = await _repo.GetWiadomosci(id);

            if (wiadomosci.OdbiorcaId != userId)
                return Unauthorized();

            wiadomosci.JestCzytana = true;
            wiadomosci.DataCzytania = DateTime.Now;

            await _repo.ZapiszWszystko();

            return NoContent();
        }

    }
}