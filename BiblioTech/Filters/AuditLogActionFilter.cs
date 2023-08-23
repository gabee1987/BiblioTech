using BiblioTech.Domain.Entities;
using BiblioTech.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BiblioTech.Filters
{
    public class AuditLogActionFilter : IAsyncActionFilter
    {
        private readonly LibraryDbContext _dbContext;

        public AuditLogActionFilter( LibraryDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync( ActionExecutingContext context, ActionExecutionDelegate next )
        {
            var auditLog = new AuditLog();

            // Store the action, controller names, and when the action was called
            auditLog.ActionName     = context.ActionDescriptor.RouteValues["action"];
            auditLog.ControllerName = context.ActionDescriptor.RouteValues["controller"];
            auditLog.CallDateTime   = DateTime.UtcNow;

            // Serialize the action parameters as a JSON string
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented          = true
            };
            auditLog.Parameters = JsonSerializer.Serialize( context.ActionArguments, options );

            // Proceed with the actual action
            var executedContext = await next();

            // Store the result (response) of the action
            if ( executedContext.Result is ObjectResult objectResult )
            {
                auditLog.Response = JsonSerializer.Serialize( objectResult.Value, options );
            }
            else
            {
                auditLog.Response = executedContext.Result.ToString();
            }

            // Add audit log to the context and save
            _dbContext.AuditLogs.Add( auditLog );
            await _dbContext.SaveChangesAsync();
        }
    }
}
