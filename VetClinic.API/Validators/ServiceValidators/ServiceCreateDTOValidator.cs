﻿using FluentValidation;
using VetClinic.API.DTO.Service;

namespace VetClinic.API.Validators.ServiceValidators
{
    public class ServiceCreateDTOValidator : AbstractValidator<ServiceCreateDTO>
    {
        public ServiceCreateDTOValidator()
        {
            RuleFor(service => service.ServiceName).NotEmpty().WithMessage("Ім'я сервісу не може бути порожнім");
            RuleFor(service => service.ServiceName).MaximumLength(50).WithMessage("Ім'я сервісу не може бути більшим 50 символів");
        }
    }
}
