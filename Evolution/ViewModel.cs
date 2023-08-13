using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionCore;
using EvolutionCore.Plants;

namespace Evolution
{
    internal static class ViewModel
    {
        static Plant _currentPlant = null;

        internal static void MainCircle()
        {
            Console.WriteLine("Press Q to exit");
            Console.WriteLine("Press N to next plant");
            Console.WriteLine("Press M to mutation");

            while (true)
            {
                var key = Console.ReadKey();

                if(key.KeyChar == 'q' || key.KeyChar == 'Q')
                {
                    break;
                }

                switch (key.KeyChar)
                {
                    case 'n':
                    case 'N':
                        {
                            Console.Clear();
                            _currentPlant = new Plant(Plant.GetRandomGenotype());
                            PrintPlant(_currentPlant);
                            break;
                        }
                    case 'm':
                    case 'M':
                        {
                            if(_currentPlant == null)
                            {
                                Console.WriteLine("There are no plant in the memory!");
                            }
                            _currentPlant = new Plant(_currentPlant.GetMutatedOffspring());
                            PrintPlant(_currentPlant);
                            break;
                        }
                }
            }

        }

        private static void PrintPlant(Plant plant)
        {
            Console.WriteLine();

            Console.WriteLine("Genotype:");
            for (int i = 0; i < plant.Genotype.GetLength(0); i++)
            {
                for (int j = 0; j < plant.Genotype.GetLength(1); j++)
                {
                    if (plant.Genotype[i, j]?.GetType() == (new PlantStructuralCell()).GetType())
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Fenotype:");
            for (int i = 0; i < plant.Fenotype.GetLength(0); i++)
            {
                for (int j = 0; j < plant.Fenotype.GetLength(1); j++)
                {
                    if (plant.Fenotype[i, j]?.GetType() == (new PlantStructuralCell()).GetType())
                    { 
                        Console.Write("#"); 
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();            
            }

            for (int j = 0; j < plant.Fenotype.GetLength(1); j++)
            {
                Console.Write("=");
            }

            Console.WriteLine();
            Console.WriteLine($"Total production: {plant.GetProducingPoints()}");
            Console.WriteLine($"Total living cost: {plant.GetLivingCost()}");
            Console.WriteLine($"Total breading cost: {plant.GetBreadingCost()}");
        }


    }
}
