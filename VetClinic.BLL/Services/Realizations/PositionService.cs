﻿using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.BLL.Services.Realizations
{
    public class PositionService : IPositionService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public PositionService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<Position> Add(Position position)
        {
            _repositoryWrapper.PositionRepository.Add(position);
            await _repositoryWrapper.SaveAsync();
            return position;
        }

        public async Task<Position> GetAsync(int positionId)
        {
            return await _repositoryWrapper.PositionRepository.GetFirstOrDefaultAsync(p => p.Id == positionId);
        }

        public async Task<ICollection<Position>> GetAsync()
        {
            return await _repositoryWrapper.PositionRepository.GetAsync();

        }

        public async Task<bool> IsAnyAsync(int positionId)
        {
            return await _repositoryWrapper.PositionRepository.IsAnyAsync(p => p.Id == positionId);
        }

        public async Task<bool> Remove(int positionId)
        {
            var position = await _repositoryWrapper.PositionRepository.GetFirstOrDefaultAsync(p => p.Id == positionId);
            if (position == null)
            {
                return false;
            }
            _repositoryWrapper.PositionRepository.Remove(position);
            return true;

        }

        public async Task<bool> Update(Position position)
        {
            if (position == null)
                return false;

            if (await _repositoryWrapper.PositionRepository.IsAnyAsync(p => p.Id == position.Id))
            {
                _repositoryWrapper.PositionRepository.Update(position);
                await _repositoryWrapper.SaveAsync();
                return true;
            }

            return false;
            
        }
    }
}
