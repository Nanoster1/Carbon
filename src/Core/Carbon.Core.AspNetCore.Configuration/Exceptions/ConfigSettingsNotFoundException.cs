namespace Carbon.Core.AspNetCore.Configuration.Exceptions;

public class ConfigSettingsNotFoundException : Exception
{
    public ConfigSettingsNotFoundException(string sectionName) :
        base($"Settings with section name \"{sectionName}\" was not found")
    {

    }
}