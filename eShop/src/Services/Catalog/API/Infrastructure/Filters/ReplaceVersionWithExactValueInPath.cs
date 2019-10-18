using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace API.Infrastructure.Filters
{
    /// <summary>
    /// Replace the end-point path from being variable as "api/v{version}/[controller]
    /// to having a fixed path e.g. "api/v1/[controller].
    /// </summary>
    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context) {
            swaggerDoc.Paths = swaggerDoc.Paths
                .ToDictionary(path => path.Key.Replace("v{version}", swaggerDoc.Info.Version),
                              path => path.Value);
        }
    }
}
