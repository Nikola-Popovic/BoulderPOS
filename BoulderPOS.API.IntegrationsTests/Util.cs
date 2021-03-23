using System.Net.Http;
using System.Text;

namespace BoulderPOS.API.IntegrationsTests
{
    public static class Util
    {
        public static StringContent JsonStringContent(string jsonObject) =>
            new StringContent(jsonObject, Encoding.UTF8, "application/json");
    }
}
