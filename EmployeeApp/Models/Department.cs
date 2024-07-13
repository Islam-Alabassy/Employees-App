using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.Models
{
    [Table("Departments",Schema ="dbo")]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Department ID")]
        public int DepartmentId { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Department Abbriviation")]
        [Column(TypeName = "varchar(5)")]
        public string DeartmentAbbr { get; set; }
    }
}
