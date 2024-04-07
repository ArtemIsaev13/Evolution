using EvolutionCore;
using EvolutionCore.Plants;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace EvolutionImageCreator;
public static class PlantImageCreator
{
    private static readonly string _imagePath = "Images";
    private static readonly int _cellWidth = 25;

    public static void SavePlantImage(Plant plant)
    {
        Bitmap image = null;

        GetPlantImage(out image, plant);

        if (!Directory.Exists(_imagePath))
        {
            Directory.CreateDirectory(_imagePath);
        }

        image.Save(Path.Combine(
            _imagePath,
            $"PlantImage_{DateTime.Now.ToString("yyyy-MM-dd_hh_mm_ss_ff") + ".bmp"}"), 
            ImageFormat.Bmp);

        image.Dispose();
    }

    public static void GetPlantImage(out Bitmap bitmap, Plant plant)
    {
        bitmap = 
            new Bitmap(
                plant.Fenotype.GetLength(1) * _cellWidth, 
                plant.Fenotype.GetLength(0) * _cellWidth);

        //Cleanong background
        using (Graphics g = Graphics.FromImage(bitmap))
        {
            Rectangle rectangle = 
                new Rectangle(
                    0, 
                    0, 
                    _cellWidth * plant.Fenotype.GetLength(1), 
                    _cellWidth * plant.Fenotype.GetLength(0));
            g.FillRectangle(new SolidBrush(Color.White), rectangle);
        }

        //Drawing all cells
        for (int i = 0; i < plant.Fenotype.GetLength(1); i++)
            for(int j = 0; j < plant.Fenotype.GetLength(0); j++)
            {
                if (plant.Fenotype[j, i] != null)
                {
                    var type = plant.Fenotype[j, i].GetType();
                    if (type == typeof(PlantStructuralCell))
                    {
                        DrowPlantStructuralCell(bitmap, i, j);
                    }
                    else if (type == typeof(PlantStoringCell))
                    {
                        DrowPlantStoringCell(bitmap, i, j);
                    }
                    else if (type == typeof(PlantPhotosyntheticCell))
                    {
                        DrowPlantPhotosyntheticCell(bitmap, i, j);
                    }
                    else
                    {
                        DrowUnknownCell(bitmap, i, j);
                    }
                }
            }
    }

    #region drawing cells
    public static void DrowPlantStructuralCell(Bitmap bitmap, int x, int y)
    {
        DrowCell(bitmap, x, y, new SolidBrush(Color.Sienna), new Pen(Color.Maroon));
    }

    public static void DrowPlantStoringCell(Bitmap bitmap, int x, int y)
    {
        DrowCell(bitmap, x, y, new SolidBrush(Color.Khaki), new Pen(Color.SandyBrown));
    }

    public static void DrowPlantPhotosyntheticCell(Bitmap bitmap, int x, int y)
    {
        DrowCell(bitmap, x, y, new SolidBrush(Color.LimeGreen), new Pen(Color.Green));
    }

    public static void DrowUnknownCell(Bitmap bitmap, int x, int y)
    {
        DrowCell(bitmap, x, y, new SolidBrush(Color.Gray), new Pen(Color.Black));
    }

    public static void DrowCell(Bitmap bitmap, int x, int y, SolidBrush fillingBrush, Pen borderPen)
    {
        using (Graphics g = Graphics.FromImage(bitmap))
        {
            Rectangle rectangle = new Rectangle(x * _cellWidth, y * _cellWidth, _cellWidth, _cellWidth);
            g.FillRectangle(fillingBrush, rectangle);
            g.DrawRectangle(borderPen, rectangle);
        }
    }
    #endregion
}
