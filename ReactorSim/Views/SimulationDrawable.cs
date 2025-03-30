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
            //canvas.DrawRectangle(0, 0, 800, 600);
            canvas.DrawRectangle(simBorder);



            /*------------------DRAWING NEUTRONS------------------*/
            for (int i = 0; i < EntitysList.neutronList.Count; i++)
            {
                Neutron neutron = EntitysList.neutronList[i];
                canvas.StrokeColor = Colors.Black;
                if (neutron.isFast)
                {
                    canvas.FillColor = Colors.White;
                    canvas.StrokeSize = 1;
                    canvas.FillCircle(neutron.x_pos, neutron.y_pos, 3);
                    canvas.DrawCircle(neutron.x_pos, neutron.y_pos, 3);
                }
                else
                {
                    canvas.FillColor = Colors.Black;
                    canvas.StrokeSize = 3;
                    canvas.FillCircle(neutron.x_pos, neutron.y_pos, 3);
                }
            }
        }
    }

}