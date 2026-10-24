using Serilog.Context;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        public const string CorrelationIdHeaderName = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 1. Lấy mã X-Correlation-ID từ request header, nếu không có thì sinh GUID mới
            var correlationId = context.Request.Headers[CorrelationIdHeaderName].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(correlationId))
            {
                correlationId = Guid.NewGuid().ToString("N");
            }

            // 2. Gắn mã vào response header để trả về cho Client
            context.Response.Headers[CorrelationIdHeaderName] = correlationId;

            // 3. Đẩy thuộc tính CorrelationId vào LogContext của Serilog
            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                await _next(context);
            }
        }
    }
}
