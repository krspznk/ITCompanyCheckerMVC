using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ITCompanyCheckerMVC.Models;

// Add profile data for application users by adding properties to the Employee class
public class Employee : IdentityUser
{
    [Key]
    public int Id { get; set; }
    [Required]
    [Column(TypeName="nvarchar(20)")]
    public string FirstName { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string LastName { get; set; }
    [Column(TypeName = "nvarchar(20)")]
    public string Login { get; set; }
    public DateTime LastUpdate { get; set; }
    public int Hours { get; set; }
    [Column(TypeName = "nvarchar(15)")]
    public string Status { get; set; }
}

