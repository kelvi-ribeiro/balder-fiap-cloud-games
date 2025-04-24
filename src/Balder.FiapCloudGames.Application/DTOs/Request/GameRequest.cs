using System.ComponentModel.DataAnnotations;

namespace Balder.FiapCloudGames.Application.DTOs.Request
{
    public sealed record GameRequest(
            [Required(ErrorMessage = "O nome é obrigatório.")]
            [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
            string Name,
            [Required(ErrorMessage = "A descrição é obrigatória.")]
            [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
            string Description,
            [Required(ErrorMessage = "A plataforma é obrigatória.")]
            [StringLength(50, ErrorMessage = "A plataforma deve ter no máximo 50 caracteres.")]
            string Platform,
            [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
            [StringLength(100, ErrorMessage = "O nome da empresa deve ter no máximo 100 caracteres.")]
            string CompanyName,
            [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
            decimal Price
        );
}