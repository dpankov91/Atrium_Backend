using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SessionsApi.Models
{
    [Table("session", Schema = "dbo")]
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("procedureId")]
        public int ProcedureId { get; set; }

        [Column("participants")]
        public int Participants { get; set; }

        [Column("additionalInfo")]
        public string AdditionalInfo { get; set; }
    }
}
