using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDB.Models;

namespace TMDB.Domain.Interfaces
{
    public interface IAuthenticationStrategy
    {
        Task<LoginSession> AuthenticateAsync(string username, string password);
    }

}
