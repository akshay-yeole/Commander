﻿using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class Command
    {
        [Key]
        public int CommandId { get; set; }

        [Required]
        public string HowTo { get; set; }

        [Required,MaxLength(250)]
        public string Line { get; set; }

        [Required]
        public string Platform { get; set; }
    }
}