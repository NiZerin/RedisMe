using RedisDesktop.Core;

namespace RedisDesktop.Infrastructure;

public static class AppPaths
{
    public const string FolderName = "RedisMe";
    public const string LegacyFolderName = "RedisDesktopForAtomUI";

    public static string Root
    {
        get
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var current = Path.Combine(appData, FolderName);
            var legacy = Path.Combine(appData, LegacyFolderName);
            if (!Directory.Exists(current) && Directory.Exists(legacy))
            {
                try
                {
                    Directory.Move(legacy, current);
                }
                catch (IOException)
                {
                    Directory.CreateDirectory(legacy);
                    return legacy;
                }
                catch (UnauthorizedAccessException)
                {
                    Directory.CreateDirectory(legacy);
                    return legacy;
                }
            }

            Directory.CreateDirectory(current);
            return current;
        }
    }

    public static string ConnectionsFile => Path.Combine(Root, "connections.json");

    public static string SettingsFile => Path.Combine(Root, "settings.json");

    public static string LogsDirectory
    {
        get
        {
            var dir = Path.Combine(Root, "logs");
            Directory.CreateDirectory(dir);
            return dir;
        }
    }

    public static string LogFile(DateTime date)
        => Path.Combine(LogsDirectory, date.ToString("yyyy-MM-dd") + ".log");

    public static string UpdatesDirectory
    {
        get
        {
            var dir = Path.Combine(Root, "updates");
            Directory.CreateDirectory(dir);
            return dir;
        }
    }
}
