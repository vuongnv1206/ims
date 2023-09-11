using System.Net;

namespace IMS.Contract.ExceptionHandling;

public class HttpException : Exception
{
    public HttpStatusCode Status { get; }

	public HttpException(HttpStatusCode statusCode, string message){}

    // bad request - 400
    public static HttpException BadRequest(string message) 
        => new HttpException(HttpStatusCode.BadRequest, message);

    // Not found - 404
    public static HttpException NotFound(string message)
        => new HttpException(HttpStatusCode.NotFound, message);

    // UnAuthorized - 401
    public static HttpException Unauthorized(string message)
        => new HttpException(HttpStatusCode.Unauthorized, message);

    // no permission - 403
    public static HttpException NoPermission(string message)
        => new HttpException(HttpStatusCode.Forbidden, message);

    // add on ....

}
