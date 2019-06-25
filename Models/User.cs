using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace login.Models
{
  public class User
  {
    [Key]
    public int UserId { get; set; }

    [Required]
    [Display(Name = "First Name:")]
    [MinLength(2, ErrorMessage = "First name must be 2 characters or longer")]
    public string fName { get; set; }

    [Required]
    [Display(Name = "Last Name:")]
    [MinLength(2, ErrorMessage = "Last name must be 2 characters or longer")]
    public string lName { get; set; }

    [Display(Name = "Email:")]
    [Required(ErrorMessage = "Email input is required")]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Password:")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be 8 characters or longer")]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [NotMapped]
    [Compare("Password")]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password, ErrorMessage = "Passwords do not match")]
    public string Confirm {get;set;}
  }
}