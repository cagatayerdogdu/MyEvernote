﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Categories")]
    public class Category : MyEntityBase
    {
        [Required, StringLength(50)]
        public string Title { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public virtual List<Note> Notes { get; set; }

        public Category()   //construckter ı sonradan MyInitialzer de fake datalar ekledikten sonra ekledik ki null note listesi oluşmasın diye
        {
            Notes = new List<Note>();
        }
    }
}