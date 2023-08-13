using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionCore.Plants
{
    public class PlantPhotosyntheticCell : PlantCell
    {
        public override float GetBreadingCost()
        {
            return 1.5f;
        }

        public override float GetLivingCost()
        {
            return 0.5f;
        }

        public override float GetProducingPoints()
        {
            return 2.0f;
        }

        public override float GetStoringSize()
        {
            return 0.0f;
        }
    }
}
