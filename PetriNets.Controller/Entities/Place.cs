using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNets.Controller.Entities
{
    public class Place : Entity
    {
        public int Tokens { get; set; }

        private Action<Place>? actionProducedToken;
        private Action<Place>? actionConsumedToken;

        public Place(string id, int tokens, Action<Place>? actionProducedToken, Action<Place>? actionConsumedToken) : base(id)
        {
            Tokens = tokens;
            this.actionProducedToken = actionProducedToken;
            this.actionConsumedToken = actionConsumedToken;
        }

        public void ConsumeToken(int quantity)
        {
            if (quantity > Tokens)
                throw new InvalidOperationException($"Erro ao consumir marcas. Valor maior que o disponível de {Tokens}");

            Tokens -= quantity;
            actionConsumedToken?.Invoke(this);
        }

        public void ConsumeAllTokens() => ConsumeToken(Tokens);

        public void ProduceToken(int quantity)
        {
            Tokens += quantity;
            actionProducedToken?.Invoke(this);
        }
    }
}
