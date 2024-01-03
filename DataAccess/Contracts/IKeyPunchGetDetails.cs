﻿using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IKeyPunchGetDetails
    {
        public Task<dynamic> GetKeyEmpData(string flag, string indata);
        public Task<dynamic> PostKeyEmpData(KeyPunchReqDto keyPunchRequesy);

    }
}
