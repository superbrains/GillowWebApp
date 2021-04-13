﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp2.Models
{
    public class VersionInfo
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }
        public string Version  { get; set; }
    }
}
