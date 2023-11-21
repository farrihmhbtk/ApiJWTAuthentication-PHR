using ApiGatewayOcelot.Models;

namespace ApiGatewayOcelot.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext _HttpContext)
        {
            dynamic[] keyClientData = UserManagement.KeyClientData();
            dynamic[] keyClientRouteData = UserManagement.keyClientRouteData();
            string cek_KeyClientName = _HttpContext.Request.Headers["KeyClientName"];

            if (string.IsNullOrEmpty(cek_KeyClientName))
            {
                await PHRKEYClientNotFound(_HttpContext);
                return;
            }

            var _keyClientData = keyClientData.FirstOrDefault(u => u.KeyClientName == cek_KeyClientName);
            if (_keyClientData != null)
            {
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
                message = "Forbiden Access."
            };
            await _HttpContext.Response.WriteAsJsonAsync(errorResponse);
        }
        private async Task PHRKEYClientNotFound(HttpContext _HttpContext)
        {
            _HttpContext.Response.StatusCode = 400;
            var errorResponse = new
            {
                message = "PHRKEYClient Not Found."
            };

            await _HttpContext.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
