using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CapStone.Modules
{
    public class RequestLine
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Request")]
        public int RequestId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }


        public int Quantity { get; set; } = 0;

        public virtual Product? Product { get; set; } = null;


        [JsonIgnore] 
        public virtual Request? Request { get; set; } = null;

    }
}
