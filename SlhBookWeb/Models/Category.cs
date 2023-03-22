using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SlhBookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        // this line To change The display in the Frontend App
        [DisplayName("Display Order")]
        //Range value of 
        [Range(1,100,ErrorMessage ="Display order must be between 1 & 100 only!")]
        public int DisplayOrder { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;  


    }
}
    