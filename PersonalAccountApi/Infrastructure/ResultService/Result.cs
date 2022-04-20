namespace PersonalAccountApi.Infrastructure.ResultService
{
    public class Result<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
    }
}
