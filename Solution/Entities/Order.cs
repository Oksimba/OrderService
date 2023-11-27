using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CardId { get; set; }
        public string Symbol { get; set; }
        public double Volume { get; set; }
        public bool Type { get; set; }
        public bool Status { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public double Profit { get; set; }

    }
   
}
