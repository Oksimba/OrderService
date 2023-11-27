using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public int CardId { get; set; }
        public string Symbol { get; set; }
        public double Volume { get; set; }
        public bool Type { get; set; }
        public double OpenPrice { get; set; }
    }
}
