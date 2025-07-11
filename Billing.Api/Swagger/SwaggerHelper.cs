using Billing.Core.Paging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace Billing.Api.Swagger
{
    internal static class SwaggerHelper
    {
        internal static void ConfigurarSwagger(SwaggerGenOptions c)
        {
            c.SwaggerDoc("v1",
                         new OpenApiInfo { Title = "Sistema de Faturamento", Version = "v1" });

            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
            var commentsFile = Path.Combine(baseDirectory, commentsFileName);

            c.IncludeXmlComments(commentsFile);

            c.CustomSchemaIds(t =>
            {
                return t.FullName;
            });
        }

        private static bool TipoIsPagedResult(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(PagedResult<>);
        }

        private static string GetTypeNamePagedResult(Type t)
        {
            var namePR = typeof(PagedResult<>).Name.Substring(0, typeof(PagedResult<>).Name.Length - 2);
            var nameType = t.GenericTypeArguments[0].Name.Replace("Dto", string.Empty);
            return $"{namePR}<{nameType}>";
        }
    }
}
