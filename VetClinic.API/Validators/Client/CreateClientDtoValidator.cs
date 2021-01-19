using FluentValidation;
using FluentValidation.Validators;
using VetClinic.API.DTO.ClientDto;
using VetClinic.API.Validators.User;

namespace VetClinic.API.Validators.Client
{
    public class CreateClientDtoValidator : CreateUserDtoValidator<CreateClientDto>//AbstractValidator<CreateClientDto>
    {
        public CreateClientDtoValidator() { }
        //public CreateClientDtoValidator()
        //{
        //    RuleFor(c => c.UserName).NotEmpty().WithMessage("Username cannot be empty")
        //       .MaximumLength(50).WithMessage("Username cannot be more than 50 symbols");

        //    RuleFor(c => c.FirstName).NotEmpty().WithMessage("FirstName cannot be empty")
        //        .MaximumLength(30).WithMessage("FirstName cannot be more than 30 symbols");

        //    RuleFor(c => c.LastName).NotEmpty().WithMessage("SecondName cannot be empty")
        //        .MaximumLength(30).WithMessage("SecondName cannot be more than 30 symbols");

        //    RuleFor(c => c.Email).NotEmpty().WithMessage("Email cannot be empty")
        //        .EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Email format is incorrect")
        //        .MaximumLength(50).WithMessage("Email cannot be longer than 50 symbols");

        //    RuleFor(c => c.PhoneNumber).NotEmpty().WithMessage("Phone cannot be empty")
        //        .MaximumLength(12).WithMessage("Phone number cannot be longer than 12 numbers")
        //        .Matches("^[0-9]{12}").WithMessage("Phone number must contain 12 numbers");

        //    RuleFor(c => c.Password).NotEmpty().WithMessage("Password cannot be empty")
        //        .MinimumLength(8).WithMessage("Password must be longer than 8 characters")
        //        .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
        //        .WithMessage("Valid password must have only upper and lower case latin characters and digits and special characters")
        //        .MaximumLength(300).WithMessage("Password is too long");
        //}
    }
}
