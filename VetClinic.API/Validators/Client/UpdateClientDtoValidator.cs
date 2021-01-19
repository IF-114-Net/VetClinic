using FluentValidation;
using FluentValidation.Validators;
using VetClinic.API.DTO.ClientDto;

namespace VetClinic.API.Validators.Client
{
    public class UpdateClientDtoValidator : AbstractValidator<UpdateClientDto>
    {
        public UpdateClientDtoValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("Username cannot be empty")
                .MaximumLength(50).WithMessage("Username cannot be more than 50 symbols");

            RuleFor(c => c.FirstName).NotEmpty().WithMessage("FirstName cannot be empty")
                .MaximumLength(30).WithMessage("FirstName cannot be more than 30 symbols");

            RuleFor(c => c.LastName).NotEmpty().WithMessage("SecondName cannot be empty")
                .MaximumLength(30).WithMessage("SecondName cannot be more than 30 symbols");

            RuleFor(c => c.Email).NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Email format is incorrect")
                .MaximumLength(50).WithMessage("Email cannot be longer than 50 symbols");

            RuleFor(c => c.PhoneNumber).NotEmpty().WithMessage("Phone cannot be empty")
                .MaximumLength(12).WithMessage("Phone number cannot be longer than 12 numbers")
                .Matches("^[0-9]{12}").WithMessage("Phone number must contain 12 numbers");
        }
    }
}
