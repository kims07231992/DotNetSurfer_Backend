using Newtonsoft.Json;

namespace DotNetSurfer_Backend.Core.Extensions
{
    public static class CommonExtension
    {
        public static string ToJson(this object @object)
        {
            return JsonConvert.SerializeObject(@object);
        }
    }
}
