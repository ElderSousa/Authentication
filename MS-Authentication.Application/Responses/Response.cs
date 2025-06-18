namespace MS_Authentication.Application.Responses;

public class Response
{
    public string Status { get; set; } = string.Empty;
    public bool Error { get; set; }

    public Response GerarErro(string notification, bool error)
    {
        Status = notification;
        Error = error;
        return this;
    }

    public Response GerarErro(List<string> notifications, bool error)
    {
        Status = string.Join("\n", notifications);
        Error = error;
        return this;
    }
}
