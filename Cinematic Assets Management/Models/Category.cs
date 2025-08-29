using System.ComponentModel.DataAnnotations;

namespace Cinematic_Assets_Management.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="لا بد ان تضع قيمه ") ]
        [MinLength(3,ErrorMessage ="يجب ان يكون اكبر من ثلاث حروف")]
        [MaxLength(20,ErrorMessage = "لا يجب ان يكون اكبر من عشرون حرف")]
        public string Name { get; set; } = string.Empty;
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();

    }
}
