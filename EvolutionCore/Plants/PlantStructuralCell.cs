using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionCore.Plants
{
    public sealed class PlantStructuralCell : PlantCell
    {
        public override float GetBreadingCost()
        {
            return 0.5f;
        }

        public override float GetLivingCost()
        {
            return 0.1f;
        }

        public override float GetProducingPoints()
        {
            return 0.5f;
        }

        public override float GetStoringSize()
        {
            return 0.5f;
        }
    }
}
