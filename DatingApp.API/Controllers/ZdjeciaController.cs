using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class ZdjeciaController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cludinaryConfig;
        private Cloudinary _cludinary;

        public ZdjeciaController(IDatingRepository repo, IMapper mapper,
            IOptions<CloudinarySettings> cludinaryConfig)
        {
            _cludinaryConfig = cludinaryConfig;
            _mapper = mapper;
            _repo = repo;

            Account acc = new Account(
                _cludinaryConfig.Value.CloudName,
                _cludinaryConfig.Value.ApiKey,
                _cludinaryConfig.Value.ApiSecret
            );

            _cludinary = new Cloudinary(acc);
 
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var zdjecieFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<ZwrocZdjecieDto>(zdjecieFromRepo);

            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> DodajZdjecieDlaUzytkownika(int userId, 
            [FromForm]DodajZdjecieDlaUzytkownikaDto dodajZdjecieDlaUzytkownikaDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var userFromRepo = await _repo.GetUser(userId);

            var file = dodajZdjecieDlaUzytkownikaDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            .Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cludinary.Upload(uploadParams);
                }
            }

            dodajZdjecieDlaUzytkownikaDto.Url = uploadResult.Uri.ToString();
            dodajZdjecieDlaUzytkownikaDto.PubliczneID = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(dodajZdjecieDlaUzytkownikaDto);

            if (!userFromRepo.Zdjecia.Any(u => u.ToMenu))
                photo.ToMenu = true;

            userFromRepo.Zdjecia.Add(photo);

            if (await _repo.ZapiszWszystko())
            {
                var photoToReturn = _mapper.Map<ZwrocZdjecieDto>(photo);
                return CreatedAtRoute("GetPhoto", new { userId = userId, id = photo.Id },
                photoToReturn);
            }

            return BadRequest("Nie udało się dodać zdjęcia");
        }

        [HttpPost("{id}/ustawGlowne")]
        public async Task<IActionResult> UstawGlowneZdjecie(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userId);

            if (!user.Zdjecia.Any(p => p.Id == id))
                return Unauthorized();

            var photoFromRepo = await _repo.GetPhoto(id);

            if (photoFromRepo.ToMenu)
                return BadRequest("To jest teraz zdjęcie główne");

            var domyslneZdjecie = await _repo.GetMainPhotoForUser(userId);
            domyslneZdjecie.ToMenu = false;

            photoFromRepo.ToMenu = true;

            if (await _repo.ZapiszWszystko())
                return NoContent();

            return BadRequest("Nie możemy ustawić zdjęcia głównego");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> UsunZdjecie(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userId);

            if (!user.Zdjecia.Any(p => p.Id == id))
                return Unauthorized();

            var photoFromRepo = await _repo.GetPhoto(id);

            if (photoFromRepo.ToMenu)
                return BadRequest("Nie możesz usunąć swojego głównego zdjęcia");

            if (photoFromRepo.PubliczneID != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.PubliczneID);

                var result = _cludinary.Destroy(deleteParams);

                if (result.Result == "ok") {
                    _repo.Usun(photoFromRepo);
                }
            }

            if (photoFromRepo.PubliczneID == null)
            {
                _repo.Usun(photoFromRepo);
            }

            if (await _repo.ZapiszWszystko())
                return Ok();

            return BadRequest("Nie udało się usunąć zdjęcia");
        }
    }
}