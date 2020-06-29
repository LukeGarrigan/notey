using System;
using System.Collections.Generic;
using System.Text;

namespace NoteyServer
{
    class AddNoteRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Markdown { get; set; }
    }
}
