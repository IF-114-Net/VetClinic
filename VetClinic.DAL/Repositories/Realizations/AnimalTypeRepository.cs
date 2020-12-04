﻿using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.DAL.Repositories.Realizations
{
    class AnimalTypeRepository : RepositoryBase<AnimalType> , IAnimalTypeRepository
    {
        public AnimalTypeRepository(ApplicationContext context) : base(context) { }
    }
}
