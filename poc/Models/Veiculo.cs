using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace poc.Models
{
    [Table("Veiculos")]
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o nome.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Obrigatório informar a placa.")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Obrigatório informar o ano de fabricação.")]
        public int AnoFabricacao { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o ano do modelo.")]
        public int AnoModelo { get; set; }
    }
}
