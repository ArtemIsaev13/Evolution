using EvolutionCore.Plants;
using EvolutionCore;
using System;
using System.Text;

namespace EvolutionWPF.Drawing;

internal static class PlantDrawer
{
    public static string GetPrintedPlant(Plant plant)
    {
        StringBuilder result = new StringBuilder();

        result.AppendLine();

        result.AppendLine("Genotype:");
        result.AppendLine(GetPrintedPlantGenotype(plant));

        result.AppendLine("Fenotype:");
        result.AppendLine(GetPrintedPlantFenotype(plant));

        result.AppendLine();
        result.AppendLine($"Total production: {plant.GetProducingPoints():f3}");
        result.AppendLine($"Total storing size: {plant.GetStoringSize():f3}");
        result.AppendLine($"Total living cost: {plant.GetLivingCost():f3}");
        result.AppendLine($"Total breading cost: {plant.GetBreadingCost():f3}");

        return result.ToString();
    }

    public static string GetPrintedPlantFenotype(Plant plant)
    {
        StringBuilder result = new();

        for (int i = 0; i < plant.Fenotype.GetLength(0); i++)
        {
            for (int j = 0; j < plant.Fenotype.GetLength(1); j++)
            {
                result.Append(ConvertPlantCellToChar(plant.Fenotype[i, j]));
            }
            result.AppendLine();
        }

        for (int j = 0; j < plant.Fenotype.GetLength(1); j++)
        {
            result.Append("=");
        }

        return result.ToString();
    }

    public static string GetPrintedPlantGenotype(Plant plant)
    {
        StringBuilder result = new();

        for (int i = 0; i < plant.Genotype.GetLength(0); i++)
        {
            for (int j = 0; j < plant.Genotype.GetLength(1); j++)
            {
                result.Append(ConvertPlantCellToChar(plant.Genotype[i, j]));
            }
            result.AppendLine();
        }

        return result.ToString();
    }

    private static char ConvertPlantCellToChar(PlantCell plantCell)
    {
        if(plantCell == null)
        {
            return ' ';
        }

        var type = plantCell.GetType();
        if(type == typeof(PlantStructuralCell))
        {
            return 'H';
        }
        else if (type == typeof(PlantStoringCell))
        {
            return 'O';
        }
        else if (type == typeof(PlantPhotosyntheticCell))
        {
            return '#';
        }
        return 'E';
    }
}
