using CustomerWebApi.DataTransferObject.Master;
using CustomerWebApi.DataTransferObject.Master.CustomerDto;
using FluentValidation;

namespace CustomerWebApi.Validators
{
    public class CreateCmrRequestDtoValidator : AbstractValidator<CreateCmrRequestDto>
    {
        private const string EmailEmpty = "Email Required";
        private const string EmailInvalid = "Invalid Email";
        private const string MobileNoEmpty = "Mobile No Required";
        private const string MobileNoLength = "Mobile No Length Must be between 10 and 15";
        private const string NameEmpty = "Name Required";
        private const string VisitedDateEmpty = "Visited Date Required";
        private const string VisitedDateInvalid = "Invalid Visited Date. Please use dd-MM-yyyy format.";

        public CreateCmrRequestDtoValidator()
        {
            RuleFor(validator => validator.Email)
                .NotEmpty().WithMessage(EmailEmpty)
                .NotNull().WithMessage(EmailEmpty)
                .Matches("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$").WithMessage(EmailInvalid);

            RuleFor(validator => validator.MobileNo)
                .NotEmpty().WithMessage(MobileNoEmpty)
                .Length(10, 15).WithMessage(MobileNoLength)
                .Matches("^[0-9]+$").WithMessage("Mobile No should contain only numbers");

            RuleFor(validator => validator.Name)
                .NotEmpty().WithMessage(NameEmpty);

            RuleFor(validator => validator.VisitedDate)
                .NotEmpty().WithMessage(VisitedDateEmpty)
                .Matches(@"^\d{2}-\d{2}-\d{4}$").WithMessage(VisitedDateInvalid); // dd-MM-yyyy format
        }
    }
}
