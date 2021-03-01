using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoulderPOS.API.Util
{
    public static class EnvironmentHelper
    {
        public static bool RunningInDocker =>
            Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
    }
}
