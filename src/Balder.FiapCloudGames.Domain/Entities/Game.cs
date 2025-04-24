using System.ComponentModel.DataAnnotations;
using Balder.FiapCloudGames.Domain.Shared.Entities;

namespace Balder.FiapCloudGames.Domain.Entities
{
    public sealed class Game : Entity
    {

        #region Propriedades
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Name { get; private set; }
        [Required(ErrorMessage = "Descrição é obrigatória!")]
        public string Description { get; private set; }
        [Required(ErrorMessage = "Plataforma é obrigatória!")]
        public string Platform { get; private set; }
        [Required(ErrorMessage = "Nome da empresa é obrigatório!")]
        public string CompanyName { get; private set; }
        [Required(ErrorMessage = "Preço é obrigatório!")]
        [Range(0.01, 9999.99, ErrorMessage = "Preço deve ser maior que 0 e menor que 9999,99!")]
        public decimal Price { get; private set; }

        #endregion
        #region Construtor
        public Game(string name, string description, string platform, string companyName, decimal price) : base(Guid.NewGuid())
        {
            Name = name;
            Description = description;
            Platform = platform;
            CompanyName = companyName;
            Price = price;
        }
        #endregion
        #region Métodos
        public void UpdateGame(Game game)
        {
            Name = game.Name;
            Description = game.Description;
            Platform = game.Platform;
            CompanyName = game.CompanyName;
            Price = game.Price;
        }
        #endregion
    }
}