using System.Text;

namespace EvolutionCore.World;

public class Location
{
    public List<Plant> Plants { get; set; } = new List<Plant>();
    public int SunlightPoints { get; set; } = 1000;

    public string CalculateFeeding()
    {
        StringBuilder log = new();

        float totalProdactivePoints = 0;
        foreach(var plant in Plants)
        {
            totalProdactivePoints += plant.GetProducingPoints();
        }
        float sunlightPointsPerProdactivePoints = SunlightPoints / totalProdactivePoints;
        log.AppendLine($"========================-NEW AGE-=============================");
        log.AppendLine($"Total plant number: {Plants.Count}");
        log.AppendLine($"Sunpoints generated: {SunlightPoints}");
        log.AppendLine($"Total productive points: {totalProdactivePoints}");
        log.AppendLine($"Sunlight points per prodactive points: {sunlightPointsPerProdactivePoints}");

        //get sunPoints to every plant
        List<Plant> extinctedPlants = new List<Plant>();
        List<Plant> newEvolvedPlants = new List<Plant>();
        StringBuilder plantLog = new();
        foreach (var plant in Plants)
        {
            plantLog.AppendLine($"-----------------One of the plant-----------------");
            var sunFood = plant.GetProducingPoints() * sunlightPointsPerProdactivePoints;
            plantLog.AppendLine($"Sunlight points gained: {sunFood}");

            // extinction phase
            sunFood -= plant.GetLivingCost();
            plantLog.AppendLine($"Living cost: {plant.GetLivingCost()}");
            if (sunFood < 0)
            {
                if(plant.StoringPoints + sunFood < 0)
                {
                    extinctedPlants.Add(plant);
                    plantLog.AppendLine($"Plant extincted");
                }
                else
                {
                    plant.StoringPoints = plant.StoringPoints + sunFood;
                    plantLog.AppendLine($"Plant storing points was decreased by {sunFood}");
                    plantLog.AppendLine($"{plant.StoringPoints}/{plant.GetStoringSize()} sunlight points in the store");
                }
                continue;
            }
            // breading phase
            plantLog.AppendLine($"Breading cost: {plant.GetBreadingCost()}");
            //We can spend stored points to breading, but we need leave living cost
            while (sunFood 
                + (plant.StoringPoints > plant.GetLivingCost() ? plant.StoringPoints - plant.GetLivingCost() : 0) 
                >= plant.GetBreadingCost())
            {
                newEvolvedPlants.Add(new Plant(plant.GetMutatedOffspring()));
                plantLog.AppendLine($"New plant arrived");
                sunFood -= plant.GetBreadingCost();

                if(sunFood < 0)
                {
                    plant.StoringPoints = plant.StoringPoints + sunFood;
                    plantLog.AppendLine($"Plant storing points was decreased by {sunFood}");
                    plantLog.AppendLine($"{plant.StoringPoints}/{plant.GetStoringSize()} sunlight points in the store");
                }
            }
            // storing phase
            plant.StoringPoints = plant.StoringPoints + sunFood;
            plantLog.AppendLine($"{sunFood} sunlights points was stored");
            plantLog.AppendLine($"{plant.StoringPoints}/{plant.GetStoringSize()} sunlight points in the store");
        }
        foreach(var plant in extinctedPlants)
        {
            Plants.Remove(plant);
        }
        foreach (var plant in newEvolvedPlants)
        {
            Plants.Add(plant);
        }

        log.AppendLine($"{extinctedPlants.Count} plants extincted");
        log.AppendLine($"{newEvolvedPlants.Count} plants was arrived");
        log.Append(plantLog);

        return log.ToString();
    }
}
