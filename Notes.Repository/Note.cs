using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes.Repository
{
    public class Note : Notes.Interfaces.INote
    {
        
        public string body
        {
            get; set;
        }

        public int? id
        {
            get; set;
        }
    }
}
