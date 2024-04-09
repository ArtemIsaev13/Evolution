using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionCore.World
{
    public class Location
    {
        public List<Plant> Plants { get; set; }
        public int SunlightPoints { get; set; } = 1000;

        public void CalculateFeeding()
        {
            float totalProdactivePoints = 0;
            foreach(var plant in Plants)
            {
                totalProdactivePoints += plant.GetProducingPoints();
            }
            float sunlightPointsPerProdactivePoints = SunlightPoints / totalProdactivePoints;

            //get sunPoints to every plant
            List<Plant> extinctedPlants = new List<Plant>();
            List<Plant> newEvolvedPlants = new List<Plant>();
            foreach(var plant in Plants)
            {
                var sunFood = plant.GetProducingPoints() * sunlightPointsPerProdactivePoints;
                // extinction phase
                sunFood -= plant.GetLivingCost();
                if(sunFood < 0)
                {
                    if(plant.StoringPoints + sunFood < 0)
                    {
                        extinctedPlants.Add(plant);
                    }
                    else
                    {
                        plant.StoringPoints = plant.StoringPoints + sunFood;
                    }
                    continue;
                }
                // breading phase
                while(sunFood >= plant.GetBreadingCost())
                {
                    newEvolvedPlants.Add(new Plant(plant.GetMutatedOffspring()));
                    sunFood -= plant.GetBreadingCost();
                }
                // storing phase
                plant.StoringPoints = plant.StoringPoints + sunFood;
            }
        }
    }
}
