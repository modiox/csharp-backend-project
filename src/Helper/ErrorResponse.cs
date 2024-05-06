namespace api.Helper
{
  public class ErrorResponse<T>
  {
    public bool? Success { get; set; } = false;
    public string? Message { get; set; }
  }
}
//status, message, data