using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BIV_Challange.Models
{
    public class CutoffValue
    {
        public int Id { get; set; }
        public int Number {  get; set; }

        [Column(TypeName = "json")]
        public string Value { get; set; }
        [JsonIgnore]
        public int CutoffId { get; set; }
    }
}
