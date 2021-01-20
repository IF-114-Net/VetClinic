﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VetClinic.BLL.Domain;
using VetClinic.BLL.Helpers;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.BLL.Services.Realizations
{
    public class AnimalService : IAnimalService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public AnimalService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task CreateAnimal(Animal animal)
        {
            _repositoryWrapper.AnimalRepository.Add(animal);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<ICollection<Animal>> GetAllAsync(
            AnimalsFilter filter = null,
            PaginationFilter pagination = null)
        {
            if (pagination != null && filter != null)
            {
                return await _repositoryWrapper.AnimalRepository.GetAsync(filter: Filter(filter), include: Include(),
                    pageNumber: pagination.PageNumber, pageSize: pagination.PageSize);
            }

            if (filter != null)
            {
                return await _repositoryWrapper.AnimalRepository.GetAsync(filter: Filter(filter), include: Include());
            }

            if (pagination != null)
            {
                return await _repositoryWrapper.AnimalRepository.GetAsync(include: Include(),
                    pageNumber: pagination.PageNumber, pageSize: pagination.PageSize);
            }

            return await _repositoryWrapper.AnimalRepository.GetAsync(include: Include());
        }

        public async Task<Animal> GetAsync(int id)
        {
            return  (await _repositoryWrapper.AnimalRepository.GetAsync(x => x.Id == id)).FirstOrDefault();
        }

        public async Task UpdateAnimal(Animal animal)
        {
            _repositoryWrapper.AnimalRepository.Update(animal);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task RemoveAnimal(Animal animal)
        {
            _repositoryWrapper.AnimalRepository.Remove(animal);
            await _repositoryWrapper.SaveAsync();
        }

        private static Func<IQueryable<Animal>, IIncludableQueryable<Animal, object>> Include()
        {
            return app => app
                .Include(a => a.AnimalType)
                .Include(a => a.Client).ThenInclude(c => c.User);
        }

        private static Expression<Func<Animal, bool>> Filter(AnimalsFilter filter)
        {
            var expressionsList = new List<Expression<Func<Animal, bool>>>();

            if (filter.ClientId != null)
            {
                Expression<Func<Animal, bool>> statusFilter = a => a.ClientId == filter.ClientId;
                expressionsList.Add(statusFilter);
            }

            if (filter.AnimalTypeId != null)
            {
                Expression<Func<Animal, bool>> serviceFilter = a => a.AnimalTypeId == filter.AnimalTypeId;
                expressionsList.Add(serviceFilter);
            }

            Expression<Func<Animal, bool>> expression = animal => true;

            foreach (var exp in expressionsList)
            {
                expression = expression.AndAlso(exp);
            }

            return expression;
        }
    }
}
