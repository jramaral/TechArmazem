﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

 using ArmazemAPI.Data;
 using ArmazemAPI.Models;
 using Microsoft.EntityFrameworkCore;


namespace ArmazemAPI.Repositories
{
    public class Repository : IRepository
    {
        public readonly ApiContext _Context;
        public Repository(ApiContext context)
        {
            _Context = context;
            _Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        
        public void Add<T>(T entity) where T : class
        {
            _Context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _Context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _Context.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _Context.SaveChangesAsync() > 0;
        }

     
        // public async Task<Matricula> GetMatricula(int userId, int cursoId)
        // {
        //     var mat = _Context.Matriculas.FirstOrDefaultAsync(x => x.AlunoId == userId && x.CursoId == cursoId);
        //     return await mat;
        // }
        //
        // public async Task<Matricula> GetMatriculaByIdAsync(int matriculaId)
        // {
        //     var query = await _Context.Matriculas.FirstOrDefaultAsync(x => x.Id == matriculaId);
        //     return query;
        // }

        // public async Task<dynamic> GetCursoByAlundoIdAsync(int userId)
        // {
        //     dynamic query = (
        //         from m in _Context.Matriculas
        //         join c in _Context.Cursos on m.CursoId equals c.Id
        //         where m.AlunoId == userId
        //         select new
        //         {
        //             Id = c.Id,
        //             NomeCurso = c.NomeCurso,
        //             Promocao = c.Promocao,
        //             Valor = c.Valor,
        //             CargaHoraria = c.CargaHoraria,
        //             ValorPromocao = c.ValorPromocao,
        //             MatriculaId = m.Id,
        //             Resumo = c.Resumo
        //             
        //         }).ToListAsync();
        //
        //     
        //     return  await query;
        //
        // }
    }
}