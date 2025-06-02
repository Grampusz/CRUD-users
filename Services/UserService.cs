using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using TestTast.DTO;
using TestTast.Helpers;
using TestTast.Models;

namespace TestTast.Services
{
    public class UserService
    {
        public (bool, string) CreateUser(CreateRequest request, string token)
        {
            if (token == null || !UserStore.Tokens
                .TryGetValue(token, out var currentUser))
                return (false, "ошибка авторизации");
            if (IsUserAdminHelper.IsAdmin(token) == false)
            {
                return (false, "только для админов");
            }
            if (UserStore.Users.Any(u => u.Login == request.login))
            {
                return (false, "логин должен быть уникальным");
            }
            UserStore.Add(request, currentUser.Name);
            return (true, "пользователь создан");
        }
        public (bool, string) GetUsers(string token)
        {
            if (token == null || !UserStore.Tokens
                .TryGetValue(token, out var currentUser))
                return (false, "ошибка авторизации");
            if (IsUserAdminHelper.IsAdmin(token) == false)
            {
                return (false, "только для админов");
            }
            var (success, users) = UserStore.GetAll();
            if (!success)
            {
                return (false, "пользователи не найдены");
            }
            string usersString = string.Join("\n\n", users.Select(u => u.ToString()));
            return (true, usersString);
        }
        public (bool, string) GetUserByLogin(ReadByLoginRequest request, string token)
        {
            if (token == null || !UserStore.Tokens
                .TryGetValue(token, out var currentUser))
                return (false, "ошибка авторизации");
            if (IsUserAdminHelper.IsAdmin(token) == false)
            {
                return (false, "только для админов");
            }
            var user = UserStore.GetUserByLogin(request.login);
            if (user == null)
            {
                return (false, "пользователь не найден");
            }
            return (true, user.ToString());
        }
        public (bool, string) GetUserByLoginAndPass(ReadByLoginAndPassRequest request, string token)
        {
            if (token == null || !UserStore.Tokens
                .TryGetValue(token, out var currentUser))
                return (false, "ошибка авторизации");
            if (IsUserActiveHelper.isUserActive(token) == false)
            {
                return (false, "Пользователь должен быть активен");
            }
            if (IsThatUserHelper.isThatUser(token, request.login) == false)
            {
                return (false, "Можно получить информацию только о себе");
            }
            var user = UserStore.GetUserByLoginAndPass(request.login, request.password);
            if (user == null)
            {
                return (false, "пользователь не найден");
            }
            return (true, user.ToString());
        }
        public (bool, string) GetUsersByAge(ReadByAgeRequest request, string token)
        {
            if (token == null || !UserStore.Tokens
                .TryGetValue(token, out var currentUser))
                return (false, "ошибка авторизации");
            if (IsUserAdminHelper.IsAdmin(token) == false)
            {
                return (false, "только для админов");
            }
            var users = UserStore.GetUsersOlderThan(request.age);
            if (users.Count == 0)
            {
                return (false, "Пользователей не найдено");
            }
            string usersString = string.Join("\n", users.Select(u => u.ToString()));
            
            return (true, usersString);
        }
        public (bool, string) UpdateProfile(UpdateProfileRequest request, string token)
        {
            bool isAdmin = true;
            bool isUserActive = true;
            bool isThatUser = true;
            if (token == null || !UserStore.Tokens
                .TryGetValue(token, out var currentUser))
                return (false, "ошибка авторизации");

            if (IsUserActiveHelper.isUserActive(token) == false)
                isUserActive = false;
                
            if (IsThatUserHelper.isThatUser(token, request.login) == false)
                isThatUser = false;
                
            if (IsUserAdminHelper.IsAdmin(token) == false)
                isAdmin = false;

            if (isAdmin || (isThatUser && isUserActive))
            {
                bool result = UserStore.UpdateProfile(request, currentUser.Name);
                if (result) return (true, "Пользователь обновлен");

                return (false, "Пользователь не найден");
            }
            else return (false, "Доступно либо администраторам, либо активному владельцу учетной записи");
        }
        public (bool, string) ChangeLogin(ChangeLoginRequest request, string token)
        {
            bool isAdmin = true;
            bool isUserActive = true;
            bool isThatUser = true;
            if (token == null || !UserStore.Tokens
                .TryGetValue(token, out var currentUser))
                return (false, "ошибка авторизации");

            if (IsUserActiveHelper.isUserActive(token) == false)
                isUserActive = false;

            if (IsThatUserHelper.isThatUser(token, request.login) == false)
                isThatUser = false;

            if (IsUserAdminHelper.IsAdmin(token) == false)
                isAdmin = false;

            if (isAdmin || (isThatUser && isUserActive))
            {
                bool result = UserStore.ChangeLogin(request, currentUser.Name);
                if (result) return (true, "Логин изменен");
                return (false, "Пользователь не найден или новый логин уже существует");
                
            }
            else return (false, "Доступно либо администраторам, либо активному владельцу учетной записи");
        }
        public (bool, string) ChangePassword(ChangePasswordRequest request, string token)
        {
            bool isAdmin = true;
            bool isUserActive = true;
            bool isThatUser = true;
            if (token == null || !UserStore.Tokens
                .TryGetValue(token, out var currentUser))
                return (false, "ошибка авторизации");

            if (IsUserActiveHelper.isUserActive(token) == false)
                isUserActive = false;

            if (IsThatUserHelper.isThatUser(token, request.login) == false)
                isThatUser = false;

            if (IsUserAdminHelper.IsAdmin(token) == false)
                isAdmin = false;

            if (isAdmin || (isThatUser && isUserActive))
            {
                bool result = UserStore.ChangePassword(request, currentUser.Name);
                if (result) return (true, "Пароль изменен");
                return (false, "Пользователь не найден"); 
            }
            return (false, "Доступно либо администраторам, либо активному владельцу учетной записи");
        }
        public (bool, string) DeleteUser(DeleteUserRequest request, string token)
        {
            if (token == null || !UserStore.Tokens
                .TryGetValue(token, out var currentUser))
            return (false, "ошибка авторизации");
            
            if (IsUserAdminHelper.IsAdmin(token) == false)
                return (false, "Доступно только администраторам");
            if (UserStore.Delete(request, currentUser.Name) == false)
                return (false, "Неизвестная ошибка");
            return (true, "Пользователь удален");

        }
        public (bool, string) RestoreUser(RestoreUserRequest request, string token)
        {
            if (token == null || !UserStore.Tokens
                .TryGetValue(token, out var currentUser))
                return (false, "ошибка авторизации");
            if (IsUserAdminHelper.IsAdmin(token) == false)
                return (false, "Доступно только администраторам");
            if (UserStore.Restore(request, currentUser.Name) == false) 
                return (false, "Неизвестная ошибка");

            return (true, "Пользователь восстановлен");
        }
    }
}
