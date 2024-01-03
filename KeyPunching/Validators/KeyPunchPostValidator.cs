using DataAccess.Dto.Request;
using FluentValidation;

namespace KeyPunching.Validators
{
    public class KeyPunchPostValidator : AbstractValidator<KeyPunchReqDetailsDto>
    {
        public KeyPunchPostValidator() 
        {
            RuleFor(d => d.brid).NotNull().NotEmpty().WithMessage("Branch ID is required");
            RuleFor(d => d.fempid).Must(EmpcodeLength).WithMessage("Assigned employee ID Length must be equal or less than 6");
            RuleFor(d => d.fempid).NotNull().NotEmpty().WithMessage("Content is required");
            RuleFor(d => d.sempid).Must(EmpcodeLength).WithMessage("Assigned employee ID Length must be equal or less than 6");
            RuleFor(d => d.sempid).NotNull().NotEmpty().WithMessage("Content is required");

        }
        private bool EmpcodeLength(string empCode)
        {
            if (Convert.ToString(empCode).Length > 6)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
