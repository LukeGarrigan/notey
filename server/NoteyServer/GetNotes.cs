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


            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(tableStorageConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(tableName);

            var noteEntity = new NoteEntity("1234", "3d375472-64e6-4d7f-aefe-175ad14e02d0");

            var columns = new List<string>();
            columns.Add("note");

            TableOperation getOperation = TableOperation.Retrieve<NoteEntity>(noteEntity.PartitionKey, noteEntity.RowKey);


            try
            {
                TableResult result = await table.ExecuteAsync(getOperation);

                var note = result.Result as NoteEntity;

                return new OkObjectResult(note);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new OkObjectResult("shit hit the fan");
            }
        }
    }

    public class NoteEntity : TableEntity
    {

        public NoteEntity() { }
        public NoteEntity(string userId, string noteId) {
            PartitionKey = userId;
            RowKey = noteId;
        }
        public string note { get; set; }
    
    }
}
