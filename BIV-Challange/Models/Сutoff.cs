using System.ComponentModel.DataAnnotations.Schema;

namespace BIV_Challange.Models
{
    public class Cutoff
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public List<CutoffValue> cutOffValues { get; set; }
    }
}
