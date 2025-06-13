using BepInEx.Logging;

namespace PingIconsOverhaul
{
    internal static class Log
    {
        private static ManualLogSource _logSource;

        internal static void Init(ManualLogSource logSource)
        {
            _logSource = logSource;
        }

        internal static void Debug(object data) => _logSource.LogDebug("DEBUG PingIconsOverhaul: " + data);
        internal static void Error(object data) => _logSource.LogError("ERROR PingIconsOverhaul: " + data);
        internal static void Fatal(object data) => _logSource.LogFatal("FATAL PingIconsOverhaul: " + data);
        internal static void Info(object data) => _logSource.LogInfo("INFO PingIconsOverhaul: " + data);
        internal static void Message(object data) => _logSource.LogMessage("MSG PingIconsOverhaul: " + data);
        internal static void Warning(object data) => _logSource.LogWarning("WARN PingIconsOverhaul: " + data);
    }
}