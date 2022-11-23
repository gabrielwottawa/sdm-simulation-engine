using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNets.Controller.Entities
{
    public class ConnectionFactory
    {
        public static Connection? Create(ConnectionType type, ConnectionData data)
        {
            switch(type)
            {
                case ConnectionType.Normal:
                    return new NormalConnection(data);

                case ConnectionType.Inibidor:
                    return new InhibitorConnection(data);

                case ConnectionType.Reset:
                    return new ResetConnection(data);
            }

            return null;
        }
    }
}
