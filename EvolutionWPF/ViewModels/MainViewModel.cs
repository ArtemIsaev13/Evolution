using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EvolutionCore;
using EvolutionImageCreator;
using EvolutionWPF.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionWPF.ViewModels;

internal sealed partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    static Plant _currentPlant = null;

    [ObservableProperty]
    private string _currentPlantText;

    public MainViewModel()
    {
        ;
    }

    [RelayCommand]
    public void CreatePlant()
    {
        CurrentPlant = new Plant(Plant.GetRandomGenotype());
        PlantImageCreator.SavePlantImage(CurrentPlant);
        CurrentPlantText = PlantDrawer.GetPrintedPlant(CurrentPlant);
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
    }

}
