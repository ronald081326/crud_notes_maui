using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCrudNotes.Models
{
    public class Note
    {
        [Key]
        public int id {  get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime creationDate { get; set; }

    }
}
