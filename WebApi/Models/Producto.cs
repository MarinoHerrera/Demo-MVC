using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApi.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(6)")]
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(6, ErrorMessage = "Maximum 6 characters only.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only Numbers.")]
        public string SKU { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters only.")]
        public string Descripcion { get; set; }

        [Column(TypeName = "decimal(10.2)")]
        [Required(ErrorMessage = "This field is required.")]
        public decimal PrecioDetal { get; set; }

        [Column(TypeName = "decimal(10.2)")]
        [Required(ErrorMessage = "This field is required.")]
        public decimal PrecioMayor { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(50, ErrorMessage = "Maximum 20 characters only.")]
        public string Estiba { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-dd-yy}")]
        public DateTime ModDate { get; set; }
    }
}
