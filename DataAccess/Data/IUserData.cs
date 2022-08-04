using DataAccess.Models;

namespace DataAccess.Data;

public interface IUserData
{
    UserModel GetUserByEmail(UserLoginModel user);
}