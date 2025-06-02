using System.ComponentModel.DataAnnotations;

namespace TestTast.DTO
{
    public record DeleteUserRequest(
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Логин должен состоять только из латинских букв или цифр")]
        string login,
        [RegularExpression("^[0-1]$", ErrorMessage = "Тип удаления должен быть цифрой от 0 до 1")]
        int deletionType);

}