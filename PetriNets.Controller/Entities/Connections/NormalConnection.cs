using PetriNets.Controller.Extensions;

namespace PetriNets.Controller.Entities
{
    public class NormalConnection : Connection
    {
        public NormalConnection(ConnectionData data) : base(data) { }

        public override bool IsEnabled { get { return connectionEnabled(); } }        

        public override void ConsumeTokens() => Place?.ConsumeToken(Weight);

        private bool connectionEnabled() => base.IsEnabled && (Place?.Tokens ?? 0) >= Weight;
    }
}
