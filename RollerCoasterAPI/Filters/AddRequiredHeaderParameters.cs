using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RollerCoasterAPI.Filters
{
    public class AddRequiredHeaderParameters : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "ClientPlatformId",
                Description = "Id of the Client Platform",
                In = ParameterLocation.Header,
                Required = true
            });
        }
    }
}
