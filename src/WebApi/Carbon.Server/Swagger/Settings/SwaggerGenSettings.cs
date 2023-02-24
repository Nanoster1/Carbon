namespace Carbon.Server.Swagger.Settings;

/// <summary>
/// Настройки SwaggerGen
/// </summary>
public sealed class SwaggerGenSettings
{
    /// <summary>
    /// Название секции в конфигурации
    /// </summary>
    public const string SectionName = "SwaggerGen";
    /// <summary>
    /// Название проекта
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// Описание проекта
    /// </summary>
    public required string Description { get; init; }
    /// <summary>
    /// Контактная информация
    /// </summary>
    public required ContactSettings Contact { get; init; }

    /// <summary>
    /// Настройки контактной информации
    /// </summary>
    public sealed class ContactSettings
    {
        /// <summary>
        /// Имя
        /// </summary>
        public required string Name { get; init; }
        /// <summary>
        /// Email
        /// </summary>
        public required string Email { get; init; }
        /// <summary>
        /// Ссылка на проект
        /// </summary>
        public required Uri Url { get; init; }
    }
}