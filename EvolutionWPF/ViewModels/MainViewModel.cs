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
    private ImageSource? _currentPlantImageSource;

    public MainViewModel()
    {
    }

    [RelayCommand]
    public void CreatePlant()
    {
        CurrentPlant = new Plant(Plant.GetRandomGenotype());
        CurrentPlantText = PlantDrawer.GetPrintedPlant(CurrentPlant);
        CurrentPlantImageSource = Converters.ImageConverter
            .ConvertBitmapToBitmapImage(PlantImageCreator.GetPlantImage(CurrentPlant));
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
        CurrentPlantImageSource = Converters.ImageConverter
            .ConvertBitmapToBitmapImage(PlantImageCreator.GetPlantImage(CurrentPlant));
    }

}
