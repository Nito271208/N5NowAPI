using Microsoft.Extensions.Configuration;
using N5Now.Domain.DTOs;
using Nest;

namespace N5Now.Infrastructure.Commons;

public class CommonsPermission
{
    #region Privates Variables

    private readonly IElasticClient _elasticsearchClient;

    #endregion

    #region Constructor

    public CommonsPermission(IElasticClient elasticsearchClient)
    {
        _elasticsearchClient = elasticsearchClient;
    }

    #endregion

    #region Principal Method ElasticSearch

    public async Task<PermissionDto> InsertIntoElasticSearch(PermissionDto dto)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var path = configuration.GetSection("Elasticsearch")["Path"];
        var index = configuration.GetSection("Elasticsearch")["DefaultIndex"];

        // Creo el documento a indexar 
        var elasticsearchDocument = new PermissionDto
        {
            Id = dto.Id,
            EmployeeForename = dto.EmployeeForename,
            EmployeeSurname = dto.EmployeeSurname,
            PermissionTypeId = dto.PermissionTypeId,
            PermissionType = dto.PermissionType,
            PermissionGrantedOnDate = dto.PermissionGrantedOnDate,
        };

        // Codigo para eliminar los documentos en ElasticSearch
        //var query = new MatchAllQuery();
        //var deleteByQueryResponse = await _elasticsearchClient.DeleteByQueryAsync<object>(d => d
        //                                                      .Index(index)
        //                                                      .Query(q => query));


        // Uso el cliente para indexar un documento en Elasticsearch
        var indexResponse = await _elasticsearchClient.IndexDocumentAsync(elasticsearchDocument);

        // Ejecuto un Refresh para que actualice los indices del "_elasticsearchClient", antes de guardar en el archivo JSON
        await _elasticsearchClient.Indices.RefreshAsync(index);

        // Valido la respuesta, solo para ver si fue exitosa
        if (indexResponse.IsValid)
        {
            Console.WriteLine("Documento indexado con éxito");
        }
        else
        {
            Console.WriteLine($"Error al indexar el documento: {indexResponse.DebugInformation}");
        }

        // Realizo la búsqueda para obtener todos los documentos
        var searchResponse = await _elasticsearchClient.SearchAsync<object>(s => s
            .Index(index)
            .Size(10000) 
            .Query(q => q.MatchAll())
        );

        // Verifico si la búsqueda fue exitosa
        if (searchResponse.IsValid)
        {
            // Guardo los documentos en formato JSON
            var json = searchResponse.Documents.Select(d => System.Text.Json.JsonSerializer.Serialize(d));

            // Registro los documentos del JSON y lo guardo en el path del appsettings.json
            File.WriteAllLines(path!, json);
            Console.WriteLine("Datos exportados correctamente.");
        }
        else
        {
            Console.WriteLine($"Error al realizar la búsqueda: {searchResponse.DebugInformation}");
        }
        return elasticsearchDocument;
    }

    public async Task<List<PermissionTypeDto>> InsertPermissionTypeIntoElasticSearch(List<PermissionTypeDto> dto)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var path = configuration.GetSection("Elasticsearch")["Path"];
        var index = configuration.GetSection("Elasticsearch")["DefaultIndex"];

        // Creo el documento a guardar 

        var elasticsearchDocuments = new List<PermissionTypeDto>();

        foreach (var permissionItems in dto)
        {
            var elasticsearchDocument = new PermissionTypeDto
            {
                Id = permissionItems.Id,
                PermissionDescription = permissionItems.PermissionDescription,
            };

            elasticsearchDocuments.Add(elasticsearchDocument);
        }

        // Codigo para eliminar los documentos en ElasticSearch
        //var query = new MatchAllQuery();
        //var deleteByQueryResponse = await _elasticsearchClient.DeleteByQueryAsync<object>(d => d
        //                                                      .Index(index)
        //                                                      .Query(q => query));


        // Uso el cliente para indexar un documento en Elasticsearch
        var indexResponse = await _elasticsearchClient.IndexDocumentAsync(elasticsearchDocuments);

        // Ejecuto un Refresh para que actualice los indices del "_elasticsearchClient", antes de guardar en el archivo JSON
        await _elasticsearchClient.Indices.RefreshAsync(index);

        // Valido la respuesta, solo para ver si fue exitosa
        if (indexResponse.IsValid)
        {
            Console.WriteLine("Documento indexado con éxito");
        }
        else
        {
            Console.WriteLine($"Error al indexar el documento: {indexResponse.DebugInformation}");
        }

        // Realizo la búsqueda para obtener todos los documentos
        var searchResponse = await _elasticsearchClient.SearchAsync<object>(s => s
            .Index(index)
            .Size(10000)
            .Query(q => q.MatchAll())
        );

        // Verifico si la búsqueda fue exitosa
        if (searchResponse.IsValid)
        {
            // Guardo los documentos en formato JSON
            var json = searchResponse.Documents.Select(d => System.Text.Json.JsonSerializer.Serialize(d));

            // Escribo los documentos en el JSON y lo guardo en el path del appsettings.json
            File.WriteAllLines(path!, json);
            Console.WriteLine("Datos exportados correctamente.");
        }
        else
        {
            Console.WriteLine($"Error al realizar la búsqueda: {searchResponse.DebugInformation}");
        }

        return elasticsearchDocuments;
    }
    #endregion
}

