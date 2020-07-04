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
    public static class DeleteNote
    {
        [FunctionName("DeleteNote")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "notes/{noteId}")] HttpRequest req,
            string noteId,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            var tableStorageConnectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
            var tableName = "notey";
            var storageAccount = CloudStorageAccount.Parse(tableStorageConnectionString);
            var table = storageAccount.CreateCloudTableClient().GetTableReference(tableName);

            try
            {
                var retrieve = TableOperation.Retrieve<NoteEntity>("1234", noteId);
                var data = await table.ExecuteAsync(retrieve);

                var note = (NoteEntity)data.Result;

                if (note != null)
                {
                    var delete = TableOperation.Delete(note);
                    var result = await table.ExecuteAsync(delete);
                }
                return new OkObjectResult("Deleted note");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new OkObjectResult("shit hit the fan");
            }
        }
    }

}
