using System.ComponentModel.DataAnnotations;

namespace ChallengeAPI.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter entre 3 e 100 caracteres")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "O campo Nome aceita apenas letras.")]
        public string Nome { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "O campo CPF deve ter entre 11 e 14 caracteres")]
        [RegularExpression(@"\d{3}\.?\d{3}\.?\d{3}-?\d{2}", ErrorMessage = "CPF em formato inválido.")]
        public string CPF { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNasc { get; set; }
    }
}