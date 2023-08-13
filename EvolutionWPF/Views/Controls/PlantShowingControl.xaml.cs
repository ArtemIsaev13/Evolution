using CommunityToolkit.Mvvm.ComponentModel;
using EvolutionCore;
using EvolutionCore.Plants;
using EvolutionWPF.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EvolutionWPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for PlantShowingControl.xaml
    /// </summary>
    public partial class PlantShowingControl : UserControl, INotifyPropertyChanged
    {
        private Plant? _plant;

        public string? PlantText { get; private set; }
        public Image? PlantImage { get; private set; }
        public Plant? Plant
        {
            get { return _plant; }
            set
            {
                if (_plant == value || value == null)
                {
                    return;
                }

                PlantText = PlantDrawer.GetPrintedPlant(value);
                _plant = value;
                OnPropertyChanged(nameof(Plant));
                OnPropertyChanged(nameof(PlantText));
                OnPropertyChanged(nameof(PlantImage));
            }
        }

        public readonly DependencyProperty PlantProperty = DependencyProperty.Register("Plant", typeof(Plant), typeof(PlantShowingControl), new FrameworkPropertyMetadata(new Plant(new PlantCell[0,0]), new PropertyChangedCallback(OnPlantChanged)));

        public event PropertyChangedEventHandler? PropertyChanged;

        public PlantShowingControl()
        {
            InitializeComponent();
            PlantText = String.Empty;
        }

        private static void OnPlantChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is PlantShowingControl c))
            {
                return;
            }

            if (!(e.NewValue is Plant value))
            {
                return;
            }

            c.Plant = value;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
