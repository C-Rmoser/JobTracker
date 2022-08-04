using DataAccess.Models;

namespace DataAccess.Data;

public class UserData : IUserData
{
    public UserModel GetUserByEmail(UserLoginModel user)
    {
        return new UserModel()
        {
            Username = "luke_admin", EmailAddress = "luke.admin@email.com", Password = "MyPass_w0rd",
            GivenName = "Luke", Surname = "Rogers", Role = "Administrator"
        };
    }
}