using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required] 
        [MaxLength(100)] 
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        [Required]
        [MaxLength(100)]
        public string CUIT { get; set; }
        public string? Address { get; set; }
        [Required]
        [MaxLength(100)]
        public string Cellphone { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
    }
}

