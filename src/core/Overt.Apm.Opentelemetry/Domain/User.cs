using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Overt.Apm.Opentelemetry.Domain
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
