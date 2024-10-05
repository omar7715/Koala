﻿using Inventory.Data.Context;
using Inventory.Data.Models;
using Inventory.Repository.Interfaces;
using KoalaInventoryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly InventoryDbContext _context;

        public GenericRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public T? GetbyId(int id)
        {
            try
            {
                return _context?.Set<T>()?.Find(id);
            }
            catch (Exception ex)
            {
                //log error
                //.......

                return default;
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                List<T> result = _context?.Set<T>()?.ToList() ?? new List<T>();
                return result;
            }
            catch (Exception ex)
            {
                //log error
                //.......

                return new List<T>();
            }
        }

        public bool Add(T entity)
        {
            try
            {
                _context?.Set<T>()?.Add(entity);
                if (_context?.Entry(entity)?.State == EntityState.Added)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                //log error
                //.......

                return false;
            }
        }

        public bool AddRange(ICollection<T> entities)
        {
            try
            {
                _context.Set<T>().AddRange(entities);
                if (_context?.Entry(entities)?.State == EntityState.Added)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                //log error
                //.......

                return false;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                T? older = _context?.Set<T>()?.Find(entity.Id);

                if (older != null)
                {
                    _context?.Set<T>()?.Update(entity);

                    if (_context?.Entry(older)?.State == EntityState.Modified)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                //log error
                //.......

                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = _context.Set<T>().Find(id);
                if (entity != null)
                {
                    _context.Set<T>().Remove(entity);
                }

                T? existing = _context?.Set<T>()?.Find(id);

                if (existing != null)
                {
                    _context?.Set<T>()?.Remove(existing);

                    if (_context?.Entry(existing).State == EntityState.Deleted)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //log error
                //.......

                return false;
            }
        }
    }
}