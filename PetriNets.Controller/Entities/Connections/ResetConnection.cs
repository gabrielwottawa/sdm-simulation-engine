namespace PetriNets.Controller.Entities
{
    public class ResetConnection : Connection
    {
        public ResetConnection(ConnectionData data) : base(data) { }

        public override void ConsumeTokens() => Place?.ConsumeAllTokens();
    }
}
