using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Contracts
{
    public interface IJwtUtils
    {
        public int? ValidateToken(string token);
    }
}
