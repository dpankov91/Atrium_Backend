using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProceduresApi.Models
{
    [Table("procedure", Schema = "dbo")]
    public class Procedure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("isCivil")]
        public int IsCivil { get; set; }

        [Column("additionalInfo")]
        public string AdditionalInfo { get; set; }
    }
}
