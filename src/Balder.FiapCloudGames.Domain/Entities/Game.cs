using System.ComponentModel.DataAnnotations;
using Balder.FiapCloudGames.Domain.Shared.Entities;

namespace Balder.FiapCloudGames.Domain.Entities
{
    public sealed class Game : Entity
    {

        #region Propriedades
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Platform { get; private set; }
        public string CompanyName { get; private set; }
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