using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.Request
{
    public class KeyPunchEmpGetDto
    {
        public string emp_code { get; set; } =string.Empty;
        public string p_flag { get; set; } = string.Empty;
    }
}
