using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionCore.Plants
{
    abstract public class PlantCell
    {
        abstract public float GetLivingCost();
        abstract public float GetBreadingCost();
        abstract public float GetStoringSize();
        abstract public float GetProducingPoints();

        public static PlantCell GetRandomPlantCell()
        {
            var rnd = new Random();
            var result = rnd.Next(3);
            switch (result)
            {
                case 0:
                    return new PlantStructuralCell();
                case 1:
                    return new PlantPhotosyntheticCell();
                case 2:
                    return new PlantStoringCell();
            }
            return new PlantStructuralCell();
        }
    }
}
