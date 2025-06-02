using System.ComponentModel.DataAnnotations;
using TestTast.Models;

namespace TestTast.DTO
{
    public record ChangeLoginRequest(
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Логин должен состоять только из латинских букв или цифр")]
        string login,
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Логин должен состоять только из латинских букв или цифр")]
        string newLogin);
}
