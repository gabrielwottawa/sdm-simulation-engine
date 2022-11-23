namespace PetriNets.Controller.Entities
{
    public class InhibitorConnection : Connection
    {
        public InhibitorConnection(ConnectionData data) : base(data) { }

        public override bool IsEnabled { get { return connectionEnabled(); } }

        public override void ConsumeTokens() { }

        private bool connectionEnabled() => base.IsEnabled && (Place?.Tokens ?? 0) <= Weight;
    }
}
