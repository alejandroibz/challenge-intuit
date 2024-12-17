using System.Text.Json.Serialization;

namespace Application.Shared
{
    public class TResponse<T>
    {
        public bool Success { get; set; } = true;
        public T? Data { get; set; }
        public Messages Messages { get; set; } = new();
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Pagination? Pagination { get; set; }
    }

    public class Messages
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = "Ejecución exitosa";
        public List<string>? Errors { get; set; } = new();
    }

    public class Pagination
    {
        public int TotalRecords { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }
}
