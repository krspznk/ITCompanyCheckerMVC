using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITCompanyCheckerMVC.Models
{
    public class EmployeeCRUD
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
    }
}
