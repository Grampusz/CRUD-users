using System.ComponentModel.DataAnnotations;

namespace TestTast.DTO
{
    public record ReadByLoginRequest(
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Логин должен состоять только из латинских букв или цифр")]
        string login);
}