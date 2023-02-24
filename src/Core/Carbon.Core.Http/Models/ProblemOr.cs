namespace Carbon.Core.Http.Models;

//TODO Добавить комментарии и подумать по поводу OneOf<T>
public class ProblemOr<TResponse>
{
    public ProblemOr(
        HttpResponseMessage httpResponse,
        TResponse? response = default,
        ProblemDetails? problem = null)
    {
        HttpResponse = httpResponse;
        Response = response!;
        Problem = problem!;
    }

    public bool IsSuccess => Problem is null;
    public TResponse Response { get; }
    public ProblemDetails Problem { get; }
    public HttpResponseMessage HttpResponse { get; }
}
