using System.ComponentModel.DataAnnotations;

namespace TestTast.DTO
{
    public record ReadByLoginAndPassRequest(
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Логин должен состоять только из латинских букв или цифр")]
        string login,
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Пароль должен состоять только из латинских букв или цифр")]
        string password);
}