using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdr_reset.Core.Models
{
    public class BusinessUnitName
    {
        [Key]
        [Column("id")]
        public string id { get; set; }

        [Column("name")]
        public string name { get; set; }
    }
}
