using CommunityToolkit.Mvvm.ComponentModel;
using ReactorSim.Models;

namespace ReactorSim.ViewModels
{
  [INotifyPropertyChanged]
  public partial class MainPageViewModel
  {
    Random rnd = new Random();
    EntitysList entitysList;
    private RectF _simulationBorder;

    [ObservableProperty]
    private int _neutronCount = 0;

    public MainPageViewModel(EntitysList el) {
      entitysList = el;
    }

    /*-------------------------------SETUP-------------------------------*/
    public void GenerateStartingSetup() 
    {
      GetWindowSize();
      GenerateNeutrons(40);
      GenerateCells();
      entitysList.simulationBorder = _simulationBorder;
    }
    private async void GetWindowSize()
    {
      await MainThread.InvokeOnMainThreadAsync(() =>
      {
        // Get the display dimenstions
        var displayInfo = DeviceDisplay.MainDisplayInfo;
        double width = displayInfo.Width / displayInfo.Density;
        double height = displayInfo.Height / displayInfo.Density;

        //Set the simulation borders
        width = width / 3 * 2;
        height = height / 7 * 6;
        _simulationBorder = new RectF(0, 0, (float)width, (float)height);
      });
    }
    private void GenerateNeutrons(int neutronCount)
    {
      for (int i = 0; i < neutronCount; i++)
      {
        entitysList.neutronList.Add(new Neutron(rnd.Next(50, (int)_simulationBorder.Width-50), rnd.Next(50, (int)_simulationBorder.Height-50), 2, (float)(Math.PI/180) * rnd.Next(0, 360), false));
      }
    }
    private void GenerateCells()
    {
      float cellSpacing;

      float horizontalDif = _simulationBorder.Width / 40;
      float verticalDif = _simulationBorder.Height / 25;

      if (horizontalDif < verticalDif)
      {
        cellSpacing = horizontalDif;
      }
      else
      {
        cellSpacing = verticalDif;
      }

      for (int i = 0; i < 40; i++)
      {
        for (int j = 0; j < 25; j++)
        {
          if(rnd.Next(1,100) <= 30)
          {
            entitysList.cellMatrix[i, j] = new ReactorSim.Models.Cell(true, false);
          }
          else
          {
            entitysList.cellMatrix[i, j] = new ReactorSim.Models.Cell(false, false);
          }
        }
      }

      entitysList.cellSpacing = cellSpacing;
    }



    /*-----------------------------SIMULATION----------------------------*/
    public void SimulationUpdate()
    {
      MoveNeutrons();
      NeutronsColison();
      NeutronCount = entitysList.neutronList.Count;
    }

    private void MoveNeutrons()
    {
      for (int i = 0; i < entitysList.neutronList.Count; i++)
      {
        Neutron neutron = entitysList.neutronList[i];
        neutron.x_pos += (float)Math.Cos(neutron.direction) * neutron.velocity;
        neutron.y_pos += (float)Math.Sin(neutron.direction) * neutron.velocity;
      }
    }
    private void NeutronsColison()
    {
      for (int i = 0; i < entitysList.neutronList.Count; i++)
      {
        Neutron neutron = entitysList.neutronList[i];

        //Check if neutron is in the simulation area
        if (!_simulationBorder.Contains(neutron.x_pos, neutron.y_pos))
        {
          entitysList.neutronList.RemoveAt(i);
        }

        //Check if neutron is colidning with uranium

      }
    }
  }
}
