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
      //GenerateNeutrons(20);
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
        entitysList.neutronList.Add(new Neutron(rnd.Next(50, (int)_simulationBorder.Width-50), rnd.Next(50, (int)_simulationBorder.Height-50), 1.5f, (float)(Math.PI/180) * rnd.Next(0, 360), true));
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
    int neutronGenCountDown = 50;
    public void SimulationUpdate()
    {
      MoveNeutrons();
      NeutronsColison();
      NeutronUpdate();
      NeutronCount = entitysList.neutronList.Count;
      
      CellUpdate();
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
        else
        {
          //Check if neutron is colidning with uranium or xenon
          float offsetNeutronX = (float)(neutron.x_pos - (entitysList.cellSpacing / 2));
          float offsetNeutronY = (float)(neutron.y_pos - (entitysList.cellSpacing / 2));
          int x = (int)Math.Round(offsetNeutronX / entitysList.cellSpacing);
          int y = (int)Math.Round(offsetNeutronY / entitysList.cellSpacing);
          
          if(x >= 0 && x < 40 && y >= 0 && y < 25)
          {
            double a = Math.Sqrt(Math.Pow((x * entitysList.cellSpacing - offsetNeutronX), 2) + Math.Pow((y * entitysList.cellSpacing - offsetNeutronY), 2));
            if (a <= entitysList.cellSpacing / 4)
            {
              //if the neutron is coliding with uranium
              if (entitysList.cellMatrix[x, y].isUranium)
              {
                if (!neutron.isFast)
                {
                  entitysList.neutronList.RemoveAt(i);
                  entitysList.cellMatrix[x, y].isUranium = false;
                  entitysList.cellMatrix[x, y].xenonCountDown = 20;

                  for (int j = 0; j < 3; j++)
                  {
                    entitysList.neutronList.Add(new Neutron(x * entitysList.cellSpacing + (entitysList.cellSpacing / 2), y * entitysList.cellSpacing + (entitysList.cellSpacing / 2), 3, (float)(Math.PI / 180) * rnd.Next(0, 360), true));
                  }
                }
              }
              //if the neutron is coliding with xenon
              else if(entitysList.cellMatrix[x, y].isXenon)
              {
                entitysList.neutronList.RemoveAt(i);
                entitysList.cellMatrix[x, y].isXenon = false;
              }
            }

            //Water temperature
            if (entitysList.cellMatrix[x, y].waterTemp < 100)
            {
              entitysList.cellMatrix[x, y].waterTemp += 0.5f;
              neutron.distanceTravelled += neutron.velocity;
            }
          }
        }
      }
    }
    private void NeutronUpdate()
    {
      for (int i = 0; i < entitysList.neutronList.Count; i++)
      {
        Neutron neutron = entitysList.neutronList[i];
        if(neutron.isFast && neutron.distanceTravelled > 150)
        {
          neutron.distanceTravelled = 0;
          neutron.velocity = 1.5f;
          neutron.isFast = false;
        }
        else if(!neutron.isFast && neutron.distanceTravelled > 150)
        {
          entitysList.neutronList.RemoveAt(i);
        }
      }
    }
    private void CellUpdate()
    {
      for (int i = 0; i < 40; i++)
      {
        for (int j = 0; j < 25; j++)
        {
          int random = rnd.Next(1, 2000);
          ReactorSim.Models.Cell cell = entitysList.cellMatrix[i, j];



          if (cell.xenonCountDown > 0)
          {
            cell.xenonCountDown--;
            if (cell.xenonCountDown == 0)
            {
              if (random > 1500)
                cell.isXenon = true;
            }
          }
          else if (!cell.isUranium && !cell.isXenon)
          {
            //Replenishing uranium
            if(random == 1)
            {
              cell.isUranium = true;
            }
            //Passive neutron generation
            else if (random == 2)
            { 
              entitysList.neutronList.Add(new Neutron(i * entitysList.cellSpacing, j * entitysList.cellSpacing, 3, (float)(Math.PI / 180) * rnd.Next(0, 360), true));
            }
          }

          if (cell.waterTemp > 20)
          {
            cell.waterTemp -= 0.2f;
          }
        }
      }
    }
  }
}
