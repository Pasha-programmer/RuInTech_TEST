using System;
using System.ComponentModel;
using System.Reflection;

namespace RuInTech_TEST.Common.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Получить описание из атрибута Description
        /// </summary>
        public static string GetDescription(this Enum value)
        {
            // Получаем тип enum
            var type = value.GetType();

            // Получаем информацию о поле
            var fieldInfo = type.GetField(value.ToString());

            // Ищем атрибут Description
            var attribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();

            // Возвращаем описание или имя поля, если атрибута нет
            return attribute?.Description ?? value.ToString();
        }
    }
}
