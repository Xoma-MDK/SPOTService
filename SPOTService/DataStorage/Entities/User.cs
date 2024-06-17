using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPOTService.DataStorage.Entities
{
    /// <summary>
    /// Пользователь системы.
    /// </summary>
    public class User : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        [Required(ErrorMessage = "Фамилия является обязательным полем.")]
        public string Surname { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required(ErrorMessage = "Имя является обязательным полем.")]
        public string Name { get; set; }

        /// <summary>
        /// Отчество пользователя.
        /// </summary>
        public string? Patronomyc { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        [Required(ErrorMessage = "Логин является обязательным полем.")]
        public string Login { get; set; }

        /// <summary>
        /// Хэш пароля пользователя.
        /// </summary>
        [Required(ErrorMessage = "Хэш пароля является обязательным полем.")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Хэш Refresh Token'а пользователя.
        /// </summary>
        public string? RefreshTokenHash { get; set; }

        /// <summary>
        /// Идентификатор роли пользователя.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Опросы, связанные с данным пользователем.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<Survey>? Surveys { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        [JsonIgnore]
        public virtual Role? Role { get; set; }
    }
}
