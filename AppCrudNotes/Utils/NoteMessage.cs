using AppCrudNotes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCrudNotes.Utils
{
    public class NoteMessage
    {

        public bool isCreate {  get; set; }
        public NoteDto noteDto { get; set; }
    }
}
