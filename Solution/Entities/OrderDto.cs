using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrderDto
    {
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
