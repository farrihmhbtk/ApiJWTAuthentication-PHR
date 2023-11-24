using ApiGatewayOcelot.Config;
using ApiGatewayOcelot.Models;

namespace ApiGatewayOcelot.Middleware
{
    public class CustomMiddlewareDB
    {
        private Context _context;
        private readonly RequestDelegate _next;
        public CustomMiddlewareDB(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext _HttpContext, Context _context)
        {
            if (true)
            {
                await _next(_HttpContext);
                return;
            }

            string cek_KeyClientName = _HttpContext.Request.Headers["KeyClientName"];
            var keyClientData = _context.GetKeyClientData();
            var keyClientRouteData = _context.GetKeyClientRouteData();

            if(!_HttpContext.Request.Headers.ContainsKey("KeyClientName"))
            {
                await NotifyNotFound(_HttpContext);
            }
            if (string.IsNullOrEmpty(cek_KeyClientName))
            {
                await NotifyNotFound(_HttpContext);
            }
            var _keyClientData = keyClientData.FirstOrDefault(u => u.KeyClientName == cek_KeyClientName);
            if (_keyClientData == null)
            {
                await NotifyUnauthorized(_HttpContext);
            }
            if (_keyClientData.Status == "Inactive")
            {
                await NotifyInactive(_HttpContext);
            }

            string requestedRoute = _HttpContext.Request.Path;
            string httpMethod = _HttpContext.Request.Method;

            bool isRouteAllowed = keyClientRouteData.Any(_keyClientRouteData =>
                _keyClientRouteData.KeyClientName == _keyClientData.KeyClientName &&
                requestedRoute.StartsWith(_keyClientRouteData.Route) &&
                _keyClientRouteData.Method.Equals(httpMethod, StringComparison.OrdinalIgnoreCase));

            if (isRouteAllowed)
            {
                await _next(_HttpContext);
            }
            else
            {
                await NotifyUnauthorized(_HttpContext);
            }
        }
        private async Task NotifyUnauthorized(HttpContext _HttpContext)
        {
            _HttpContext.Response.StatusCode = 401;
            var errorResponse = new
            {
                message = "Your KeyClientName does not have access."
            };
            await _HttpContext.Response.WriteAsJsonAsync(errorResponse);
        }
        private async Task NotifyNotFound(HttpContext _HttpContext)
        {
            _HttpContext.Response.StatusCode = 400;
            var errorResponse = new
            {
                message = "KeyClientName not found."
            };

            await _HttpContext.Response.WriteAsJsonAsync(errorResponse);
        }
        private async Task NotifyInactive(HttpContext _HttpContext)
        {
            _HttpContext.Response.StatusCode = 400;
            var errorResponse = new
            {
                message = "KeyClientName is inactive."
            };

            await _HttpContext.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
