using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BIV_Challange.Models
{
    public class CutoffForProduct
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int Number { get; set; }
        [JsonIgnore]
        public int ProductId { get; set; }
        public int CutoffId { get; set; }
        [Column(TypeName = "json")]
        public string Value { get; set; }
    }
}
