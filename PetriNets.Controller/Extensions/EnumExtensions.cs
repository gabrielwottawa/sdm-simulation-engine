using PetriNets.Controller.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNets.Controller.Extensions
{
    public static class ConnectionExtensions
    {
        public static string ConvertToString(this Connection connection)
        {
            switch (connection)
            {
                case NormalConnection:
                    return "Normal";

                case InhibitorConnection:
                    return "Inibidor";

                case ResetConnection:
                    return "Reset";
            }

            return "";
        }
    }
}
