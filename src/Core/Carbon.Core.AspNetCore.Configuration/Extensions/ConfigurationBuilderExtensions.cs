using Microsoft.Extensions.Configuration;

using static Carbon.Core.FileSystem.Defaults.FileSystemDefaults.Extensions;

namespace Carbon.Core.AspNetCore.Configuration.Extensions;

public static class ConfigurationBuilderExtensions
{
    /// <summary>
    /// Добавляет файл в конфигурацию, автоматически определяя расширение. <br/>
    /// Если необходимо можно добавить файл окружения в формате FileName.Environment.Extension
    /// </summary>
    /// <param name="path">Путь до файла, относительный или абсолютный</param>
    /// <param name="addEnvironmentFile">
    /// Добавление файла окружения. <br/>
    /// Если <paramref name="environment"/> = <see langword="null"/>, то <see cref="NullReferenceException"/>
    /// </param>
    /// <param name="environment">
    /// Имя окружения. Нужно только если добавляем файл окружения. <br/>
    /// <see cref="IHostEnvironment.EnvironmentName"/>
    /// </param>
    /// <remarks>Путь относителен <see cref="FileConfigurationExtensions.SetBasePath(IConfigurationBuilder, string)"/></remarks>
    /// <exception cref="NotSupportedException" />
    /// <exception cref="NullReferenceException" />
    public static IConfigurationBuilder AddFile(
        this IConfigurationBuilder builder,
        string path,
        bool optional = false,
        bool reloadOnChange = false,
        bool addEnvironmentFile = false,
        string? environment = null)
    {
        var ext = Path.GetExtension(path);
        var notSupportedException = new NotSupportedException($"Расширение {ext} не поддерживается");

        switch (ext)
        {
            case Json: builder.AddJsonFile(path, optional, reloadOnChange); break;
            case Yaml or Yml: builder.AddYamlFile(path, optional, reloadOnChange); break;
            case Xml: builder.AddXmlFile(path, optional, reloadOnChange); break;
            case Ini: builder.AddIniFile(path, optional, reloadOnChange); break;
            default: throw notSupportedException;
        };

        if (!addEnvironmentFile) return builder;
        if (environment is null) throw new NullReferenceException("При добавлении файла окружения необходимо указать имя окружения");

        path = Path.ChangeExtension(path, $".{environment}{ext}");
        switch (ext)
        {
            case Json: builder.AddJsonFile(path, true, reloadOnChange); break;
            case Yaml or Yml: builder.AddYamlFile(path, true, reloadOnChange); break;
            case Xml: builder.AddXmlFile(path, true, reloadOnChange); break;
            case Ini: builder.AddIniFile(path, true, reloadOnChange); break;
            default: throw notSupportedException;
        }

        return builder;
    }
}