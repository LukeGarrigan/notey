using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace NoteyServer
{

    public class NoteEntity : TableEntity
    {

        public NoteEntity() { }
        public NoteEntity(string userId, string noteId)
        {
            PartitionKey = userId;
            RowKey = noteId;
        }
        public string Markdown { get; set; }
        public string Title { get; set; }

        public DateTime LastUpdateTime { get; set; }

    }
}
