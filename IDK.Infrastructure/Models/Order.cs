using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDK.Infrastructure.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderPlaced {  get; set; }
        public DateTime? OrderFulfilled { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;

        public IEnumerable<OrderProduct> ProductIds { get; set; } = new List<OrderProduct>();

        [Column(TypeName = "decimal(6,2)")]
        [Required]
        public decimal TotalPrice { get; set; }

        public bool? IsComplete { get; set; }
    }
}
