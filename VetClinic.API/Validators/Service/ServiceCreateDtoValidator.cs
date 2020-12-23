﻿using FluentValidation;
using VetClinic.API.DTO.Service;

namespace VetClinic.API.Validators.ServiceValidators
{
    public class ServiceCreateDtoValidator : AbstractValidator<ServiceCreateDto>
    {
        public ServiceCreateDtoValidator()
        {
            RuleFor(service => service.ServiceName).NotEmpty().WithMessage("The service name can not be empty")
               .MaximumLength(50).WithMessage("The service name can not be longer than 50 characters");
        }
    }
}
