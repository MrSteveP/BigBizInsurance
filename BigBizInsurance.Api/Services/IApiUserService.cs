using BigBizInsurance.Api.Entities;
using BigBizInsurance.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBizInsurance.Api.Services
{
    public interface IApiUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    }
}
