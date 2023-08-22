namespace BiblioTech.DTO
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
