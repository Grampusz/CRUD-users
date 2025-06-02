using System.ComponentModel.DataAnnotations;
using TestTast.Models;

namespace TestTast.DTO
{
    public record ChangePasswordRequest(
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Логин должен состоять только из латинских букв или цифр")]
        string login,
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Пароль должен состоять только из латинских букв или цифр")]
        string newPassword);
}
