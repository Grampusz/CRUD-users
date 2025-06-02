using System.ComponentModel.DataAnnotations;
using TestTast.Models;

namespace TestTast.DTO
{
    public record UpdateProfileRequest(
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Логин должен состоять только из латинских букв или цифр")]
        string login,
        [RegularExpression("^[a-zA-Zа-яА-ёЁЯ]+$", ErrorMessage = "Имя должно состоять только из латинских или русских букв")]
        string? name,
        [RegularExpression("^[0-2]$", ErrorMessage = "Пол должен быть цифрой от 0 до 2")]
        int? gender,
        DateTime? birthDay);
}
