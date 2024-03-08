using N5Now.Infrastructure.Commands;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace N5NowAPI.Filter
{
    #region Clase con su Metodo para remover campos desde el Swagger
    public class RemoveFieldsFromCommandFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            // Aquí filtramos los campos del esquema CreatePermissionCommand
            if (context.Type == typeof(CreatePermissionCommand))
            {
                // Verificamos si existe la propiedad "createPermission" en el esquema
                if (schema.Properties.TryGetValue("createPermission", out var createPermissionProperty)
                    && createPermissionProperty.Reference?.Id != null)
                {
                    // Obtengo el esquema de la referencia
                    var createPermissionSchema = context.SchemaRepository.Schemas[createPermissionProperty.Reference.Id];

                    // Elimino las propiedades  que no quiero ver en el TryOut 
                    createPermissionSchema.Properties.Remove("id");
                    createPermissionSchema.Properties.Remove("permissionType");
                    createPermissionSchema.Properties.Remove("message");
                    createPermissionSchema.Properties.Remove("permissions"); 
                }
            }

            // Aquí filtramos los campos del esquema UpdatePermissionCommand
            if (context.Type == typeof(UpdatePermissionCommand))
            {
                // Verificamos si existe la propiedad "updatePermission" en el esquema
                if (schema.Properties.TryGetValue("updatePermission", out var updatePermissionProperty)
                    && updatePermissionProperty.Reference?.Id != null)
                {
                    // Obtengo el esquema de la referencia
                    var updatePermissionSchema = context.SchemaRepository.Schemas[updatePermissionProperty.Reference.Id];

                    // Elimino las propiedades  que no quiero ver en el TryOut 
                    updatePermissionSchema.Properties.Remove("id");
                    updatePermissionSchema.Properties.Remove("permissionType");
                    updatePermissionSchema.Properties.Remove("message");
                    updatePermissionSchema.Properties.Remove("permissions");
                }
            }
        }
    }
    #endregion
}
