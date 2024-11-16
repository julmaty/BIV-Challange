using System.ComponentModel.DataAnnotations.Schema;

namespace BIV_Challange.Models
{
    public class TableForParam
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Number { get; set; }
        [Column(TypeName = "json")]
        public string Value { get; set; }
        [Column(TypeName = "json")]
        public string CutoffForProductNumbers { get; set;}
    }
}
