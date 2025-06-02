namespace TestTast.Models
{
    public class User
    {
        public Guid Guid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool Admin { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? RevokedOn { get; set; }
        public string? RevokedBy { get; set; }
        public override string ToString()
        {
            return $"Логин: {Login}\n" +
                   $"Пароль: {Password}\n" +
                   $"Имя: {Name}\n" +
                   $"Пол: {Gender}\n" +
                   $"Дата рождения: {(BirthDay?.ToShortDateString() ?? "не указана")}\n" +
                   $"Админ: {(Admin ? "Да" : "Нет")}\n" +
                   $"Создан: {CreatedOn.ToString("g")} (кем: {CreatedBy})\n" +
                   $"Изменён: {ModifiedOn.ToString("g")} (кем: {ModifiedBy})\n" +
                   $"Статус: {(RevokedOn.HasValue ? $"Удалён {RevokedOn.Value.ToString("g")} (кем: {RevokedBy ?? "неизвестно"})" : "Активен")}";
        }
    }
        public enum Gender { Male = 0, Female = 1, unknown = 2 };
}
