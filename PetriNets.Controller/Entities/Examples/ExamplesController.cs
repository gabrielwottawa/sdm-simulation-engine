using PetriNets.Controller.Entities.Examples.LoadExamples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNets.Controller.Entities.Examples
{
    public class ExamplesController
    {
        private string example = string.Empty;

        public ExamplesController(string example)
        {
            this.example = example;
        }

        public PetriNet GetPetriNet()
        {
            switch (example)
            {
                case "Arco Reset":
                    var resetArc = new ResetArc();
                    return resetArc.Load_ResetArc();
                case "Arco Inibidor":
                    var inhibitorArc = new InhibitorArc();
                    return inhibitorArc.Load_InhibitorArc();
                case "Concorrência":
                    var concurrence = new Concurrence();
                    return concurrence.Load_Concurrence();
                case "Enunciado":
                    var statement = new Statement();
                    return statement.Load_Statement();
                case "Padrão":
                    var patter = new Pattern();
                    return patter.Load_Pattern();
                case "Rede Referência TA":
                    var referenceNetTA = new ReferenceNetTA();
                    return referenceNetTA.Load_ReferenceNetTA();
            }

            return new PetriNet();
        }
    }
}
