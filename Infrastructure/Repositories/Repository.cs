﻿using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class Repository<TEntity>(DbSet<TEntity> entities) : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> _entities = entities;

    public async Task AddAsync(TEntity entity) => await _entities.AddAsync(entity);
    public async Task UpdateAsync(TEntity entity) => await Task.Run(() => { _entities.Update(entity); });
    public async Task<TEntity> GetByIdAsync(int id, IEnumerable<string> entitiesToInclude) => await _entities.Include(entitiesToInclude)
                                                                                                             .FirstOrDefaultAsync(e => e.Id == id);
    public async Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<Expression<Func<TEntity, bool>>> predicates,
                                                        IEnumerable<string> entitiesToInclude) => await _entities.Filter(predicates)
                                                                                                                 .Include(entitiesToInclude)
                                                                                                                 .ToListAsync();
    public async Task<IEnumerable<TEntity>> GetAllAsync(int skip, int limit, IEnumerable<Expression<Func<TEntity, bool>>> predicates,
                                                                             IEnumerable<string> entitiesToInclude) => await _entities.Filter(predicates)
                                                                                                                                      .Skip(skip)
                                                                                                                                      .Take(limit)
                                                                                                                                      .Include(entitiesToInclude)
                                                                                                                                      .ToListAsync();
    public void Delete(TEntity entity) => _entities.Remove(entity);
    public async Task DeleteAsync(TEntity entity) => await Task.Run(() => { Delete(entity); });
    public async virtual Task<int> Count(IEnumerable<Expression<Func<TEntity, bool>>> predicates = null) => await _entities.Filter(predicates).CountAsync();
    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate) => await _entities.AnyAsync(predicate);
}
