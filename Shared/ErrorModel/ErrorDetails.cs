
using System.Text.Json;

namespace Shared.ErrorModel
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public override string ToString()=> JsonSerializer.Serialize(this);
       


    }
}
