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

     
    }
}