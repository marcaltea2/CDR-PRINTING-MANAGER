using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdr_reset.Core.Models
{
    public class PrintLog
    {
        [Key]
        [Column("document_num")]
        public string document_num { get; set; }

        [Column("business_unit")]
        public string business_unit { get; set; }

        [Column("business_unit_id")]
        public string business_unit_id { get; set; }

        [Column("times_printed")]
        public byte times_printed { get; set; }

        [Column("printed_by")]
        public string printed_by { get; set; }

        [Column("printed_date")]
        public DateTime? printed_date { get; set; }

        [Column("re_printed_by")]
        public string re_printed_by { get; set; }

        [Column("re_printed_date")]
        public DateTime? re_printed_date { get; set; }
    }
}
