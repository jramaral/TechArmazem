﻿using System.Collections.Generic;
using System.Threading.Tasks;
 using ArmazemAPI.Models;

 namespace ArmazemAPI.Repositories
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangeAsync();

       
    }
}