using Microsoft.Extensions.Configuration;

namespace PhotoBackup.Library.Extensions;

public static class ConfigurationExtenstions
{

    public static T BuildSettings<T>(this IConfiguration config)
    {
        T settingsGroup = config.GetSection(typeof(T).Name).Get<T>()!;

        return settingsGroup;
    }
}
