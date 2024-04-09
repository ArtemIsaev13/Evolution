using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EvolutionCore;
using EvolutionImageCreator;
using EvolutionWPF.Drawing;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EvolutionWPF.ViewModels;

internal sealed partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    static Plant? _currentPlant = null;

    [ObservableProperty]
    private string? _currentPlantText;

    [ObservableProperty]
    private Bitmap? _currentPlantImage;

    [ObservableProperty]
    private ImageSource? _currentPlantImageSource;

    public MainViewModel()
    {
        CurrentPlantImage = new Bitmap("C:\\UserData\\_Another\\Programming\\Evolution\\EvolutionWPF\\bin\\Debug\\net7.0-windows\\Images\\PlantImage_2024-04-09_11_01_31_59.bmp");
    }

    [RelayCommand]
    public void CreatePlant()
    {
        CurrentPlant = new Plant(Plant.GetRandomGenotype());
        CurrentPlantText = PlantDrawer.GetPrintedPlant(CurrentPlant);
        var bitmap = PlantImageCreator.GetPlantImage(CurrentPlant);
        CurrentPlantImageSource = Converters.ImageConverter.ConvertBitmapToBitmapImage(bitmap);
        PlantImageCreator.SaveImage(bitmap);
    }

    [RelayCommand]
    public void EvolveCurrentPlant()
    {
        if (CurrentPlant == null)
        {
            CurrentPlantText = "There are no plant in the memory!";
            return;
        }
        CurrentPlant = new Plant(CurrentPlant.GetMutatedOffspring());
        CurrentPlantText = PlantDrawer.GetPrintedPlant(CurrentPlant);
        var bitmap = PlantImageCreator.GetPlantImage(CurrentPlant);
        CurrentPlantImageSource = Converters.ImageConverter.ConvertBitmapToBitmapImage(bitmap);
        PlantImageCreator.SaveImage(bitmap);
    }

}
