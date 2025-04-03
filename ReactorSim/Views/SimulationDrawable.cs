using Microsoft.Maui.Graphics;
using ReactorSim.Models;

namespace ReactorSim.Views
{
  public class SimulationDrawable : IDrawable
  {
    public EntitysList EntitysList { get; set; }

    public void Draw(ICanvas canvas, RectF simBorder)
    {
      /*------------------DRAWING SIMULATION BORDERS------------------*/
      canvas.StrokeColor = Colors.Aqua;
      canvas.StrokeSize = 2;
      canvas.DrawRectangle(EntitysList.simulationBorder.X, EntitysList.simulationBorder.Y, EntitysList.simulationBorder.Width, EntitysList.simulationBorder.Height);


      /*------------------DRAWING CELLS------------------*/
      for (int i = 0; i < EntitysList.cellList.Count; i++)
      {
        ReactorSim.Models.Cell cell = EntitysList.cellList[i];
        if (cell.isUranium)
        {
          canvas.FillColor = Colors.Blue;
          canvas.FillRectangle(cell.x_pos, cell.y_pos, EntitysList.cellSpacing, EntitysList.cellSpacing);
        }
        else if (cell.isXenon)
        {
          canvas.FillColor = Colors.Black;
          canvas.FillRectangle(cell.x_pos, cell.y_pos, EntitysList.cellSpacing, EntitysList.cellSpacing);
        }
        else
        {
          canvas.FillColor = Colors.White;
          canvas.FillRectangle(cell.x_pos, cell.y_pos, EntitysList.cellSpacing, EntitysList.cellSpacing);
        }
      }

      /*------------------DRAWING NEUTRONS------------------*/
      for (int i = 0; i < EntitysList.neutronList.Count; i++)
      {
        Neutron neutron = EntitysList.neutronList[i];
        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 1;
        if (neutron.isFast)
        {
          canvas.FillColor = Colors.White;
          canvas.FillCircle(neutron.x_pos, neutron.y_pos, 3);
          canvas.DrawCircle(neutron.x_pos, neutron.y_pos, 3);
        }
        else
        {
          canvas.FillColor = Colors.Black;
          canvas.FillCircle(neutron.x_pos, neutron.y_pos, 3);
        }
      }
    }
  }
}