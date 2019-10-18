using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace API.Infrastructure.Filters
{
    /// <summary>
    /// Remove api version as a parameter from the Swagger documentation.
    /// </summary>
    public class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context) {
            var parameterVersion = operation.Parameters.Single(parameter => parameter.Name == "version");
            operation.Parameters.Remove(parameterVersion);
        }
    }
}
