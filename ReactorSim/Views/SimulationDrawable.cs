﻿using Microsoft.Maui.Graphics;
using ReactorSim.Models;

namespace ReactorSim.Views
{
  public class SimulationDrawable : IDrawable
  {
    public EntitysList EntitysList { get; set; }

    public void Draw(ICanvas canvas, RectF simBorder)
    {
      /*------------------DRAWING SIMULATION BORDERS------------------*/
      //canvas.StrokeColor = Colors.Aqua;
      //canvas.StrokeSize = 2;
      //canvas.DrawRectangle(EntitysList.simulationBorder.X, EntitysList.simulationBorder.Y, EntitysList.simulationBorder.Width, EntitysList.simulationBorder.Height);


      /*------------------DRAWING CELLS------------------*/
      for(int i = 0; i < 40; i++)
      {
        for (int j = 0; j < 25; j++)
        {
          ReactorSim.Models.Cell cell = EntitysList.cellMatrix[i, j];

          float temp = cell.waterTemp - 20;
          if (temp < 80)
          {
            if(temp <= 12)
            {
              canvas.FillColor = new Color((int)(225 + (temp * 2.5)), 225, 225);
            }
            else
            {
              canvas.FillColor = new Color(255, (int)(255 - (temp * 2.5)), (int)(255 - (temp * 2.5)));
            }
            canvas.FillRectangle(i * EntitysList.cellSpacing, j * EntitysList.cellSpacing, EntitysList.cellSpacing, EntitysList.cellSpacing);
          }

          if (cell.isUranium)
          {
            canvas.FillColor = new Color(56, 119, 255);
          }
          else if (cell.isXenon)
          {
            canvas.FillColor = Colors.Black;
          }
          else
          {
            canvas.FillColor = Colors.LightGray;
          }

          canvas.FillCircle(i * EntitysList.cellSpacing + (EntitysList.cellSpacing / 2), j * EntitysList.cellSpacing + (EntitysList.cellSpacing / 2), EntitysList.cellSpacing / 4);
        }
      }

      /*------------------DRAWING GRAPHITE RODS-------------------*/
      for (int i = 0; i < 11; i++)
      {
        canvas.FillColor = Colors.White;
        canvas.FillRectangle((i * 4 - 0.125f) * EntitysList.cellSpacing, 0, EntitysList.cellSpacing / 4, EntitysList.cellSpacing * 25);
        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = EntitysList.cellSpacing / 40;
        canvas.DrawRectangle((i * 4 - 0.125f) * EntitysList.cellSpacing, 0, EntitysList.cellSpacing / 4, EntitysList.cellSpacing * 25);
      }

      /*------------------DRAWING CONTROL RODS------------------*/
      for(int i = 0; i < EntitysList.controlRodsArray.Length; i++) {
        ControlRod controlRod = EntitysList.controlRodsArray[i];
        float x_pos = (i * 4 - 0.125f + 2) * EntitysList.cellSpacing;

        canvas.FillColor = Colors.Black;
        canvas.FillRectangle(x_pos, controlRod.y_pos, EntitysList.cellSpacing / 4, EntitysList.cellSpacing * 25);
        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = EntitysList.cellSpacing / 10;
        canvas.DrawLine(x_pos + (EntitysList.cellSpacing / 8), controlRod.y_pos + (EntitysList.cellSpacing * 25), x_pos + (EntitysList.cellSpacing / 8), controlRod.y_pos + (EntitysList.cellSpacing * 26));

        canvas.FillColor = Colors.White;
        canvas.FillRectangle(x_pos, controlRod.y_pos + (EntitysList.cellSpacing * 26), EntitysList.cellSpacing / 4, EntitysList.cellSpacing * 5);
        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = EntitysList.cellSpacing / 40;
        canvas.DrawRectangle(x_pos, controlRod.y_pos + (EntitysList.cellSpacing * 26), EntitysList.cellSpacing / 4, EntitysList.cellSpacing * 5);
      }

      /*------------------DRAWING NEUTRONS------------------*/
      for (int i = 0; i < EntitysList.neutronList.Count; i++)
      {
        Neutron neutron = EntitysList.neutronList[i];
        if(neutron != null) {
          canvas.StrokeColor = Colors.Black;
          canvas.StrokeSize = 1;
          if (neutron.isFast)
          {
            canvas.FillColor = Colors.White;
            canvas.FillCircle(neutron.x_pos, neutron.y_pos, EntitysList.cellSpacing / 10);
            canvas.DrawCircle(neutron.x_pos, neutron.y_pos, EntitysList.cellSpacing / 10);
          }
          else
          {
            canvas.FillColor = Colors.Black;
            canvas.FillCircle(neutron.x_pos, neutron.y_pos, EntitysList.cellSpacing / 10);
          }
        }
      }
    }
  }
}