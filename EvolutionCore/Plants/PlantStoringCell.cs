using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionCore.Plants
{
    public sealed class PlantStoringCell : PlantCell
    {
        public override float GetBreadingCost()
        {
            return 2;
        }

        public override float GetLivingCost()
        {
            return 0.0f;
        }

        public override float GetProducingPoints()
        {
            return .0f;
        }

        public override float GetStoringSize()
        {
            return 10f;
        }
    }
}
