using Microsoft.AspNetCore.Identity.Data;
using TestTast.DTO;

namespace TestTast.Models
{
    public static class UserStore
    {
        public static List<User> Users { get; }
        public static Dictionary <string, User> Tokens{ get; }
        private static void ModifyUser(User user, string modifiedBy)
        {
            user.ModifiedBy = modifiedBy;
            user.ModifiedOn = DateTime.Now;
        }
        public static User? GetUserByLogin(string login) =>
            Users.FirstOrDefault(u => u.Login == login);
        public static User? GetUserByLoginAndPass(string login, string password) =>
            Users.FirstOrDefault(u => u.Login == login && u.Password == password);
        public static List<User> GetUsersOlderThan(int age)
        {
            return Users
                .Where(u => u.BirthDay.HasValue &&
                    (DateTime.Today.Year - u.BirthDay.Value.Year -
                    (DateTime.Today < u.BirthDay.Value.AddYears(DateTime.Today.Year - u.BirthDay.Value.Year) ? 1 : 0)) > age)
                .ToList();
        }
        public static (bool, List<User>?) GetAll()
        {
            var users = Users.Where(u => u.RevokedOn == null).OrderBy(u => u.CreatedOn).ToList();
            if (users.Count == 0) return (false, null);
            return (true, users);

        }
        public static bool Add(CreateRequest createRequest, string modifiedBy)
        {
            Users.Add(new User
            {
                Guid = Guid.NewGuid(),
                Login = createRequest.login,
                Password = createRequest.password,
                Name = createRequest.name,
                Gender = (Gender)createRequest.gender,
                BirthDay = createRequest.birthDay,
                Admin = createRequest.isAdmin,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                CreatedBy = modifiedBy,
                ModifiedBy = modifiedBy,
            });
            return true;
        }
        public static bool UpdateProfile(UpdateProfileRequest request, string modifiedBy)
        {
            var user = Users.FirstOrDefault(u => u.Login== request.login);
            if (user!= null)
            {
                if (string.IsNullOrEmpty(request.name) == false 
                    && request.name!=user.Name)
                {
                    user.Name = request.name;
                    ModifyUser(user, modifiedBy);
                }
                if (request.gender.HasValue 
                    && user.Gender !=(Gender)request.gender)
                {
                    user.Gender = (Gender)request.gender;
                    ModifyUser(user, modifiedBy);
                }
                if (request.birthDay.HasValue
                    && user.BirthDay !=request.birthDay)
                {
                    user.BirthDay = request.birthDay;
                    ModifyUser(user, modifiedBy);
                }
                
                return true;
            }
            return false;
        }
        public static bool ChangeLogin(ChangeLoginRequest request, string modifiedBy)
        {
            var user = Users.FirstOrDefault(u => u.Login == request.login);
            if (user == null)
            {
                return false;
            }
            if (Users.FirstOrDefault(u => u.Login == request.newLogin) is User)
            {
                return false;
            }
            user.Login = request.newLogin;
            ModifyUser(user, modifiedBy);            
            return true;
        }
        public static bool ChangePassword(ChangePasswordRequest request, string modifiedBy)
        {
            var user = Users.FirstOrDefault(u => u.Login == request.login);
            if (user is null)
            {
                return false;
            }
            user.Password = request.newPassword; 
            ModifyUser(user, modifiedBy);
            return true;
        }
        public static bool Delete(DeleteUserRequest request, string modifiedBy)
        {
            var user = Users.FirstOrDefault(u => u.Login == request.login);
            if (user is null)
            {
                return false;
            }
            if (request.deletionType==0)
            {
                Users.Remove(user);
                var key = Tokens.FirstOrDefault(pair => pair.Value.Login == request.login).Key;
                Tokens.Remove(key);
            }
            if (request.deletionType==1)
            {
                user.RevokedBy = modifiedBy;
                user.RevokedOn = DateTime.Now;
                ModifyUser(user, modifiedBy);
            }
            return true;
        }
        public static bool Restore(RestoreUserRequest request, string modifiedBy)
        {
            var user = Users.FirstOrDefault(u => u.Login == request.login);
            if (user is null) return false;
            user.RevokedBy = null;
            user.RevokedOn = null;
            ModifyUser(user, modifiedBy);
            return true;
        }
        public static User? Login(DTO.LoginRequest request)
        {
            return Users.FirstOrDefault(u =>
                u.Login == request.login &&
                u.Password == request.password &&
                u.RevokedOn == null
            );
        }
        public static void RemoveUserTokens(string login)
        {
            foreach (var item in Tokens.Where(item => item.Value.Login == login))
            {
                Tokens.Remove(item.Key);
            }
        }
        public static void AddToken(User user, string token)
        {
            Tokens[token] = user;
        }
        static UserStore()
        {
            Users = new List<User>();
            Tokens = new Dictionary<string, User>();

            User user = new User
            {
                Guid = Guid.NewGuid(),
                Login = "admin",
                Password = "admin",
                Name = "admin",
                Gender = (Gender)2,
                BirthDay = DateTime.Now,
                Admin = true,
                CreatedOn = DateTime.Now,
                CreatedBy = "system",
                ModifiedOn = DateTime.Now,
                ModifiedBy = "system"
            };

            Users.Add(user);
        }
    }
}
