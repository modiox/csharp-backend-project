namespace api.Helper
{
    public class SuccessResponse<T>
    {
        public bool? Success { get; set; } = true;
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
//status, message, data