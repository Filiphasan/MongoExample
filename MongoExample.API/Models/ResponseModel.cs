namespace MongoExample.API.Models;

public class ResponseModel<TResponse> where TResponse : class
{
    public int StatusCode { get; set; }
    public ResponseErrorModel Error { get; set; }
    public TResponse Result { get; set; }

    private ResponseModel(int statusCode, TResponse response)
    {
        StatusCode = statusCode;
        Result = response;
    }

    private ResponseModel(int statusCode, ResponseErrorModel error)
    {
        StatusCode = statusCode;
        Error = error;
    }

    private ResponseModel(int statusCode, string errorMessage, string? errorDetail)
    {
        StatusCode = statusCode;
        Error = new ResponseErrorModel()
        {
            Message = errorMessage,
            Detail = errorDetail,
        };
    }

    public static ResponseModel<TResponse> SendSuccess(TResponse response)
    {
        return new ResponseModel<TResponse>(200, response);
    }

    public static ResponseModel<TResponse> SendError(int statusCode, ResponseErrorModel error)
    {
        return new ResponseModel<TResponse>(statusCode, error);
    }

    public static ResponseModel<TResponse> SendError(int statusCode, string errorMessage, string? errorDetail = null)
    {
        return new ResponseModel<TResponse>(statusCode, errorMessage, errorDetail);
    }

    public static ResponseModel<TResponse> SendException(Exception exception)
    {
        return new ResponseModel<TResponse>(500, exception.Message, null);
    }
}

public class ResponseErrorModel
{
    public string Message { get; set; }
    public string? Detail { get; set; }
}