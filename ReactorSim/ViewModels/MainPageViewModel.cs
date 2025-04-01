using CommunityToolkit.Mvvm.ComponentModel;
using ReactorSim.Models;
using System.Diagnostics;

namespace ReactorSim.ViewModels
{
  [INotifyPropertyChanged]
  public partial class MainPageViewModel
  {
    Random rnd = new Random();
    EntitysList entitysList;

    [ObservableProperty]
    private int _neutronCount = 0;

    public MainPageViewModel(EntitysList el) {
      entitysList = el;
    }

    /*-------------------------------SETUP-------------------------------*/
    public void GenerateStartingSetup() 
    {
      GenerateNeutrons(40);
    }
    private void GenerateNeutrons(int neutronCount)
    {
      for (int i = 0; i < neutronCount; i++)
      {
        entitysList.neutronList.Add(new Neutron(rnd.Next(1, 800), rnd.Next(1,600), 2, (float)(Math.PI/180) * rnd.Next(0, 360), false));
      }
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
      //Check if neutron is out of borders
      RectF rectF = entitysList.simulationBorders;
      for (int i = 0; i < entitysList.neutronList.Count; i++)
      {
        Neutron neutron = entitysList.neutronList[i];
        if (!rectF.Contains(neutron.x_pos, neutron.y_pos))
        {
          entitysList.neutronList.RemoveAt(i);
        }
      }
    }
  }
}
