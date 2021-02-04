using System;
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
    public class Compra : ControllerBase
    {
        public readonly ICompraVendaRepository _Repo;
  
        private readonly IMapper _mapper;
        public Compra(ICompraVendaRepository repository, IMapper mapper)
        {
            _Repo = repository;

            _mapper = mapper;
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(CompraVendaDto model)
        {
            try
            {
                var cp = _mapper.Map<CompraVenda>(model);
                
                _Repo.Add(cp);
                if (await _Repo.SaveChangeAsync())
                {
                    return Created($"v1/api/{cp.Id}", _mapper.Map<CompraVendaDto>(cp));
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server Error,{e.Message}");
            }
        
            return BadRequest();
        }
        [HttpPost]
        [Route("item/add")]
        public async Task<IActionResult> AddItem(ItemDto model)
        {
            try
            {
                var ite = _mapper.Map<Item>(model);
            
                if (!_Repo.IsCompra(ite.CompraVendaId))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"O codigo {ite.CompraVendaId} não é uma compra");
                }
                
                _Repo.Add(ite);
                if (await _Repo.SaveChangeAsync())
                {
                    _Repo.AtualizaEstoque(ite.ProdutoId);
                   
                    return Created($"v1/api/{ite.Id}", _mapper.Map<ItemDto>(ite));
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