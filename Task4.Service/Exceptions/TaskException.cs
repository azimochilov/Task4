namespace Task4.Service.Exceptions;
public class TaskException : Exception
{
    public int Code { get; set; }
    public TaskException(int code = 500, string message = "Something went wrong") : base(message)
    {
        this.Code = code;
    }
}
