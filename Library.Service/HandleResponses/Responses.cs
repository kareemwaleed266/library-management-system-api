namespace Library.Service.HandleResponses
{
    public class Responses : Exception
    {
        public Responses(int statusCode, string? message = null)
            : base(message ?? GetDefaultMessageForStatusCode(statusCode))
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }

        private static string GetDefaultMessageForStatusCode(int code)
        {
            return code switch
            {
                200 => "OK",
                201 => "Created",
                204 => "No Content",
                301 => "Moved Permanently",
                302 => "Found",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                502 => "Bad Gateway",
                503 => "Service Unavailable",
                _ => "Unknown Status Code"
            };
        }
    }
}
