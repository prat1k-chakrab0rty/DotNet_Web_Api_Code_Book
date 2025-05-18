using DotNet_Web_Api_Code_Book.Common.Models;

namespace DotNet_Web_Api_Code_Book.Service.Interface
{
    public interface IJWTTokenService
    {
        public string GenerateToken(User user);
    }
}
