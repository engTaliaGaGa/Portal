using System;

namespace WebApi.Singleton.Interface
{
    public interface IAuthService
    {
        bool ValidateLogin(string user, string pass);
        string GenerateToken(DateTime date, string user, int? idApp, TimeSpan validDate);
    }
}
