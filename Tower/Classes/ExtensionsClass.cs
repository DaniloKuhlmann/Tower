using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;

namespace Tower.Classes;

public static class ExtensionsClass
{
    /// <summary>
    /// Função que determina o valor do parametro levando em consideração a annotation DisplayName
    /// </summary>
    /// <param name="value">Valor do parametro</param>
    /// <returns>Retorna o display name do parametro, caso não consiga trazer esse valor, retorna o nome do parametro</returns>
    public static string? GetEnumDisplayName(Enum value)
    {
        if (value != null)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);
            return attributes != null && attributes.Length > 0 ? attributes[0].Name : value.ToString();
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// Retorna a descrição definida ana Annotation
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetDescription(Enum value)
    {
        var enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();
        var descriptionAttribute =
            enumMember == null
                ? default
                : enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
        return
            descriptionAttribute == null
                ? value.ToString()
                : descriptionAttribute.Description;
    }
    /// <summary>
    /// Obtem a extensão fazendo a conversão literal da string para enum, se falhar tenta obter o tipo pelo display name.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="DisplayName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static T? GetEnumValueFromDisplayName<T>(string DisplayName)
    {
        try
        {
            var enumtype = (T)Enum.Parse(typeof(T), DisplayName, true);
            return enumtype;
        }
        catch
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new Exception("Variavel não um tipo de enum");
            }
            var fields = type.GetFields();
            var select = fields.First(x => x.CustomAttributes.SelectMany(z => z.NamedArguments.Select(x => x.TypedValue.Value)).Any(x => x?.ToString() == DisplayName));
            return select == null ? default : (T)select.GetRawConstantValue();
        }
    }
}