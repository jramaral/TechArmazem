using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArmazemAPI.Dto;
using ArmazemAPI.Models;
using ArmazemAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArmazemAPI.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class ClienteFornecedorController : ControllerBase
    {
        
        public readonly ICliForRepository _Repo;
        private readonly IMapper _mapper;

        public ClienteFornecedorController(ICliForRepository repository, IMapper mapper)
        {
            _Repo = repository;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("cliente")]
        [AllowAnonymous]
        public async Task<IActionResult> TodosCliente()
        {
            try
            {
                var cli = await _Repo.ClienteTodos();
                  var results = _mapper.Map<IEnumerable<CliForDto>>(cli);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("fornecedor")]
        public async Task<IActionResult> TodosFornecedores()
        {
            try
            {
                var forn = await _Repo.FornecedorTodos();
                var results = _mapper.Map<IEnumerable<CliForDto>>(forn);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Gravar(CliForDto model)
        {
            try
            {
                var clifor = _mapper.Map<ClienteFornecedor>(model);
                _Repo.Add(clifor);
                if (await _Repo.SaveChangeAsync())
                {
                    return Created($"v1/api/{clifor.Id}", _mapper.Map<CliForDto>(clifor));
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server Error,{e.Message}");
            }
        
            return BadRequest();
        }
    }
}