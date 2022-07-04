using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ITCompanyCheckerMVC.Models;

public class Employee : IdentityUser
{
    public int Id { get; set; }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CardId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Login { get; set; }
    public DateTime LastUpdate { get; set; }
    public int Hours { get; set; }
    public string Status { get; set; }
    public float Salary { get; set; }
}

