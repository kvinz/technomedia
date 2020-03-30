using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technomedia.Domain.Entities
{
    public class User
    {
        /// <summary>
        /// ID пользователя/записи
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required(ErrorMessage = "Пожалуйста, введите имя пользователя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(ErrorMessage = "Пожалуйста, введите фамилию пользователя")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "Отчество")]
        public string SecondName { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        [Required(ErrorMessage = "Пожалуйста, введите логин пользователя")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage = "Пожалуйста, введите пароль пользователя")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Уникальный токен
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public int RoleId { get; set; }
    }
}
