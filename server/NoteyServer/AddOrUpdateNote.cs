using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace NoteyServer
{
    public static class AddOrUpdateNote
    {
        [FunctionName("AddOrUpdateNote")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var addNoteRequest = JsonConvert.DeserializeObject<AddNoteRequest>(requestBody);


            var tableStorageConnectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
            var tableName = "notey";
            var storageAccount = CloudStorageAccount.Parse(tableStorageConnectionString);
            var table = storageAccount.CreateCloudTableClient().GetTableReference(tableName);

            var noteId = addNoteRequest.Id;

            if (!string.IsNullOrEmpty(addNoteRequest.Id))
            {
                var retrieve = TableOperation.Retrieve<NoteEntity>("1234", addNoteRequest.Id);

                var data = await table.ExecuteAsync(retrieve);

                var note = (NoteEntity)data.Result;


                if (note != null) 
                {
                    note.Markdown = addNoteRequest.Markdown;
                    note.Title = addNoteRequest.Title;
                    note.LastUpdateTime = DateTime.UtcNow;
                   
                    var replace = TableOperation.Replace(note);

                    await table.ExecuteAsync(replace);
                }

            }
            else {

                noteId = Guid.NewGuid().ToString();
                var insert = TableOperation.Insert(new NoteEntity
                {
                    PartitionKey = "1234",
                    RowKey = noteId,
                    Markdown = addNoteRequest.Markdown,
                    Title = addNoteRequest.Title,
                    LastUpdateTime = DateTime.UtcNow
                });


                await table.ExecuteAsync(insert);
            }


            return new OkObjectResult(noteId);
        }
    }
}
