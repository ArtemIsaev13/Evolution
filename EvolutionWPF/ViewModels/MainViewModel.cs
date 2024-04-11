using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EvolutionCore;
using EvolutionCore.World;
using EvolutionImageCreator;
using EvolutionWPF.Drawing;
using System.Windows.Media;

namespace EvolutionWPF.ViewModels;

internal sealed partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    static Plant? _currentPlant;

    [ObservableProperty]
    private string? _currentPlantText;

    [ObservableProperty]
    private ImageSource? _currentPlantImageSource;

    [ObservableProperty]
    private Location? _currentLocation;

    [ObservableProperty]
    private string? _currentLocationString;

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

    [RelayCommand]
    public void CreateLocation()
    {
        CurrentLocation = new Location();
        for(int i = 0; i < 10; i++)
        {
            CurrentLocation.Plants.Add(new Plant(Plant.GetRandomGenotype()));
        }
        CurrentPlantText = "New location was created";
    }

    [RelayCommand]
    public void CalculateOneAge()
    {
        if(CurrentLocation == null)
        {
            CurrentLocationString = "There are no location in the memory";
            return;
        }
        CurrentPlantText = CurrentLocation.CalculateFeeding();
    }

}
