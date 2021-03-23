using System;

namespace BoulderPOS.API.Util
{
    public static class EnvironmentHelper
    {
        public static bool RunningInDocker =>
            Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
    }
}
