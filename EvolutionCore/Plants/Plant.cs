using EvolutionCore.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionCore
{
    public class Plant
    {
        public PlantCell[,] Fenotype;
        public PlantCell[,] Genotype;

        private double _storingPoints;
        public double StoringPoints
        {
            get => _storingPoints; 
            set
            {
                if(value > 0)
                {
                    _storingPoints = value;
                }
                if(_storingPoints > GetStoringSize())
                {
                    _storingPoints = GetStoringSize();
                }
            }
        }

        public Plant(PlantCell[,] seed)
        {
            Genotype = seed;
            Fenotype = GetCleanedFenotype(seed);
        }

        public PlantCell[,] GetMutatedOffspring()
        {
            var result = (PlantCell[,])Genotype.Clone();
            var rnd = new Random();
            var i = rnd.Next(result.GetLength(0) - 1);
            var j = rnd.Next(result.GetLength(1) - 1);

            if(result[i, j] == null)
            {
                result[i, j] = PlantCell.GetRandomPlantCell();
            }
            else
            {
                result[i, j] = null;
            }

            return result;
        }


        public float GetProducingPoints()
        {
            var result = 0f;
            for(int i = 0; i < Fenotype.GetLength(1); i++)
            {
                int shadowCoeff = 1;
                for(int j = 0; j < Fenotype.GetLength(0); j++)
                {
                    if (Fenotype[j,i] != null)
                    {
                        result += Fenotype[j, i].GetProducingPoints() / shadowCoeff++;
                    }
                }    
            }
            return result;
        }

        public float GetStoringSize()
        {
            var result = 0f;
            foreach (var cell in Fenotype)
            {
                result += cell?.GetStoringSize() ?? 0;
            }
            return result;
        }

        public float GetLivingCost()
        {
            var result = 0f;
            foreach (var cell in Fenotype)
            {
                result += cell?.GetLivingCost() ?? 0;
            }
            return result;
        }

        public float GetBreadingCost()
        {
            var result = 0f;
            foreach (var cell in Fenotype)
            {
                result += cell?.GetBreadingCost() ?? 0;
            }
            return result;
        }

        public static PlantCell[,] GetRandomGenotype()
        {
            var random = new Random();
            var result = new PlantCell[random.Next(10) + 1, random.Next(10) + 1];

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    switch (random.Next(4))
                    {
                        case 0:
                            {
                                result[i, j] = PlantCell.GetRandomPlantCell();
                                break;
                            }
                    }
                }
            }

            return result;
        }

        //Очищает генотип до фенотипа.
        //-Удаляет все элементы, не имеющие связи с субстратом
        private PlantCell[,] GetCleanedFenotype(PlantCell[,] genotype)
        {
            var previousFen = new PlantCell[genotype.GetLength(0), genotype.GetLength(1)];

            //Нижняя строка просто копируется
            for (int j = 0; j < genotype.GetLength(1); j++)
            {
                if (genotype[genotype.GetLength(0) - 1, j] == null)
                {
                    continue;
                }
                previousFen[genotype.GetLength(0) - 1, j] = genotype[genotype.GetLength(0) - 1, j];
            }

            var currentFen = (PlantCell[,])previousFen.Clone();
            while (true)
            {
                previousFen = (PlantCell[,])currentFen.Clone();

                for (int i = genotype.GetLength(0) - 2; i >= 0; i--)
                {
                    for (int j = 0; j < genotype.GetLength(1); j++)
                    {
                        if (genotype[i, j] == null)
                        {
                            continue;
                        }
                        else if(currentFen[i, j] != null)
                        {
                            continue;
                        }
                        else if(
                            (i > 0 && currentFen[i-1, j] != null) || 
                            (i < currentFen.GetLength(0) - 1 && currentFen[i + 1, j] != null) ||
                            (j > 0 && currentFen[i, j - 1] != null) ||
                            (j < currentFen.GetLength(1) - 1 && currentFen[i, j + 1] != null))
                        {
                            currentFen[i, j] = genotype[i, j];
                        }
                    }
                }
                if(IsEqualFenotype(currentFen, previousFen))
                {
                    break;
                }
            }

            return currentFen;
        }

        private bool IsEqualFenotype(PlantCell[,] a, PlantCell[,] b)
        {
            if(a == null || b == null)
            {
                throw new ArgumentNullException("Array is null");
            }

            if ((a.GetLength(0) != b.GetLength(0)) || (a.GetLength(1) != b.GetLength(1)))
            {
                return false;
            }

            for(int i = 0; i < a.GetLength(0); i++)
                for(int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] == null && b[i, j] == null)
                    {
                        continue;
                    }
                    if (a[i, j] == null || b[i, j] == null)
                    {
                        return false;
                    }
                    if(a[i, j].GetType != b[i, j].GetType)
                    {
                        return false;
                    }
                }
            return true;
        }
    }
}
