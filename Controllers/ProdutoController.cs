using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmazemAPI.Dto;
using ArmazemAPI.Dto.POCO;
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
    public class ProdutoController : ControllerBase
    {
        public readonly IProdutoRepository _Repo;
        private readonly IMapper _mapper;
        
        public ProdutoController(IProdutoRepository repo, IMapper mapper)
        {
            _Repo = repo;
            _mapper = mapper;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var produtos = await _Repo.RetornarTodosProdutosAsync();
              
                var results = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var curso = await _Repo.RetornarProdutoPorIdAsync(id);
                var result = _mapper.Map<ProdutoDto>(curso);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server Error, {e.Message}");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> NovoProduto(ProdutoDto model)
        {
            try
            {
                var produto = _mapper.Map<Produto>(model);
                _Repo.Add(produto);
                if (await _Repo.SaveChangeAsync())
                {
                    return Created($"v1/api/{produto.Id}", _mapper.Map<ProdutoDto>(produto));
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server Error,{e.Message}");
            }
        
            return BadRequest();
        }
        
       
        [HttpPost]
        [Route("desconto")]
       public  IActionResult AplicarDesconto(PocoProd prod)
        {
            try
            {
                if(_Repo.AplicarDesconto(prod))
                {
                    return Ok($"Desconto Aplicado no produto:  {prod.ProdutoId}");
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server Error,{e.Message}");
            }
            return BadRequest();
        }

       [HttpDelete]
       [Route("{Id}")]
       public async Task<IActionResult> Excluir(int Id)
       {
           try
           {
               if (await _Repo.TemMovimento(Id))
               {
                   return  StatusCode(StatusCodes.Status400BadRequest, "Não é possivel remover. Existe uma movimentação vinculada para esse produto!");
               }
               
               var result = await _Repo.RetornarProdutoPorIdAsync(Id);
               if (result == null) return NotFound();

               _Repo.Delete(result);
               if (await _Repo.SaveChangeAsync())
               {
                   return Ok();
               }

           }
           catch (Exception)
           {
               return StatusCode(StatusCodes.Status500InternalServerError, "Impossível executar essa operação agora!");
           }

           return BadRequest();

       }
       
       [HttpPut("{id}")]
       public async Task<IActionResult> Put(int id, ProdutoDto model)
       {
           try
           {
               var produto = await _Repo.RetornarProdutoPorIdAsync(model.Id);
              
               if (produto == null) return NotFound();

               _mapper.Map(model, produto);
               _Repo.Update(produto);
               if (await _Repo.SaveChangeAsync())
               {
                   return Created($"/api/produto/{model.Id}", _mapper.Map<ProdutoDto>(model));
               }
           }
           catch (Exception)
           {
               return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no servidor!");
           }

           return BadRequest();
         
       }

    }
}