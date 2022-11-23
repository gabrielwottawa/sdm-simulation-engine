using PetriNets.Controller.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace PetriNets.Controller.Xml
{
    public class XmlFilePetriNet
    {
        private string filePath;
        private XmlDocument document;
        private List<string> placeIds = new();
        private List<string> transitionIds = new();

        public PetriNet PetriNet { get; private set; }

        public XmlFilePetriNet(string filePath)
        {
            this.filePath = filePath;
            PetriNet = new PetriNet();
        }

        public void Load()
        {
            using (var reader = new StreamReader(filePath))
            {
                document = new XmlDocument();
                document.Load(reader);
                createPlaces();
                createTransitions();
                createConnections();
            }
        }

        private void createPlaces()
        {
            iterateElements("place", place =>
            {
                var id = place["id"]?.InnerText ?? string.Empty;
                if (!string.IsNullOrEmpty(id))
                {
                    placeIds.Add(id);
                    var tokens = Convert.ToInt32(place["tokens"]?.InnerText ?? "0");
                    PetriNet.CreatePlace(id, tokens);
                }
            });
        }

        private void createTransitions()
        {
            iterateElements("transition", transition =>
            {
                var id = transition["id"]?.InnerText ?? string.Empty;
                if (!string.IsNullOrEmpty(id))
                {
                    transitionIds.Add(id);
                    PetriNet.CreateTransition(id);
                }
            });
        }

        private void createConnections()
        {
            iterateElements("arc", connection =>
            {
                var source = connection["sourceId"]?.InnerText ?? string.Empty;
                var destination = connection["destinationId"]?.InnerText ?? string.Empty;
                var weight = Convert.ToInt32(connection["multiplicity"]?.InnerText ?? "1");
                var info = getConnectionInfo(source, destination);
                var type = getConnectionType(connection);

                if (info.Place != null && info.Transition != null)
                    PetriNet.CreateConnection(info.Place, info.Transition, weight, type, info.Direction);
            });
        }

        private (Place? Place, Transition? Transition, ConnectionDirection Direction) getConnectionInfo(string source, string destination)
        {
            var direction = ConnectionDirection.Input;
            var place = PetriNet.GetPlace(source);
            var transition = PetriNet.GetTransition(destination);

            if (place == null || transition == null)
            {
                place = PetriNet.GetPlace(destination);
                transition = PetriNet.GetTransition(source);
                direction = ConnectionDirection.Output;
            }

            return (place, transition, direction);
        }

        private ConnectionType getConnectionType(XmlNode connection)
        {
            var type = connection["type"]?.InnerText ?? "regular";
            switch (type)
            {
                case "regular":
                    return ConnectionType.Normal;

                case "inhibitor":
                    return ConnectionType.Inibidor;

                case "reset":
                    return ConnectionType.Reset;
            }

            return ConnectionType.Normal;
        }

        private void iterateElements(string tagName, Action<XmlNode> action)
        {
            var nodes = document.GetElementsByTagName(tagName);
            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes.Item(i);
                if (node != null)
                    action(node);
            }
        }
    }
}
