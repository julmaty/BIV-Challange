using System.ComponentModel.DataAnnotations.Schema;

namespace BIV_Challange.Models
{
    public class CutoffForProduct
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int ProductId { get; set; }
        public int CutoffId { get; set; }
        [Column(TypeName = "json")]
        public string Value { get; set; }
    }
}
