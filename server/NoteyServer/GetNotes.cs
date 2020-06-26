using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;

namespace NoteyServer
{
    public static class GetNotes
    {
        [FunctionName("GetNotes")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "notes")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var tableStorageConnectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
            var tableName = "notey";
            var storageAccount = CloudStorageAccount.Parse(tableStorageConnectionString);

            var table = storageAccount.CreateCloudTableClient().GetTableReference(tableName);

            var query = new TableQuery<NoteEntity>().Where(
              TableQuery.GenerateFilterCondition(
                "PartitionKey",
                QueryComparisons.Equal,
                "1234"
              ));

            try
            {
                var result = await table.ExecuteQuerySegmentedAsync(query, null);
                var noteEntities = new List<NoteEntity>();

                result.Results.ForEach(note => noteEntities.Add((note as NoteEntity)));

                return new OkObjectResult(noteEntities);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new OkObjectResult("shit hit the fan");
            }
        }
    }

}
