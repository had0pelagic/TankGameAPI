using Newtonsoft.Json;

namespace TankGameAPI.Utils
{
    public class Error
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
