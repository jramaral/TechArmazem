using System;
using System.Threading.Tasks;
using ArmazemAPI.Dto;
using ArmazemAPI.Models;
using ArmazemAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfissaAPI.Services;

namespace ArmazemAPI.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class UserController : Controller
    {
      
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        
        public UserController(IUserRepository repo, IMapper mapper)
        {
            
            _repo = repo;
            _mapper = mapper;
        }
        
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async  Task<ActionResult<dynamic>> Login([FromBody]UsuarioDto model)
        {
            var user = await _repo.GetUsuario(model.NomeUsuario, model.Senha);
            var result = _mapper.Map<UsuarioDto>(user);
            if (result == null)
            {
                return NotFound(new {message = "usuario ou senha inválido"});
            }

            var tocken = TokenService.GenerateToken(user);
            result.Senha = "";

            return new
            {
                user = result,
                tocken = tocken
            };
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UsuarioDto userDto)
        {
            try
            {
                var user = _mapper.Map<Usuario>(userDto);
                _repo.Add(user);
                if (await _repo.SaveChangeAsync())
                {
                    var userAdded = _mapper.Map<UsuarioDto>(user);
                    return Created("GetUser", userAdded);
                   // return Created($"v1/api/{user.Id}", _mapper.Map<UsuarioDto>(user));
                }
               
                return BadRequest();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}