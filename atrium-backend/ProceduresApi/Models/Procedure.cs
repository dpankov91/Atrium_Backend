using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProceduresApi.Models
{
    [Table("customer", Schema = "dbo")]
    public class Procedure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("isCivil")]
        public bool isCivil { get; set; }

        [Column("additional_info")]
        public string AdditionalInfo { get; set; }
    }
}
