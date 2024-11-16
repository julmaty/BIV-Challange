using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BIV_Challange.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        [Column(TypeName = "json")]
        public string OblFields { get; set; }
        [JsonIgnore]
        public int UserCreated { get; set; }
        [JsonIgnore]
        public int UserUpdated { get; set; }
        public List<CutoffForProduct> CutoffsForProduct { get; set;}
        public List<TableForParam> TablesForParam { get; set; }
    }
}
