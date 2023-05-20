using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lab2Client.Models.ViewModels;

public class EditEmployeeViewModel 
{
    [Required]
    public int EmployeeId { get; set; }

    [Required]
    [MinLength(4)]
    [MaxLength(100)]
    [DisplayName("First Name")]
    public string FirstName { get; set; } = null!;

    [Required]
    [MinLength(4)]
    [MaxLength(100)]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = null!;

    [Required]
    [DisplayName("Date Of Employment")]
    public DateTime DateOfEmployment { get; set; }
}
