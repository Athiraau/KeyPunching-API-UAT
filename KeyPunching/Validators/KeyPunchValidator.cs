using DataAccess.Dto.Request;
using FluentValidation;

namespace KeyPunching.Validators
{
    public class KeyPunchValidator :AbstractValidator<KeyPunchEmpGetDto>
    {

        public KeyPunchValidator()
        {
            //RuleFor(d => d.emp_code).NotNull().NotEmpty().WithMessage("Employee ID is required");
           // RuleFor(d => Convert.ToString(d.emp_code)).Must(EmpcodeLength).WithMessage("Assigned employee ID Length must be equal or less than 6");
            RuleFor(d => d.p_flag).NotNull().NotEmpty().WithMessage("Flag is required");

            
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
