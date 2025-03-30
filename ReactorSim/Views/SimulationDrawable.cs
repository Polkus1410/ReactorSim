using ReactorSim.Models;

namespace ReactorSim.Views
{
    public class SimulationDrawable : IDrawable
    {
      
        public EntitysList EntitysList { get; set; }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        { 
            canvas.StrokeColor = Colors.Blue;
            canvas.StrokeSize = 6;
            foreach (var neutron in EntitysList.neutronList)
            {
                canvas.DrawCircle(new PointF(neutron.x_pos, neutron.y_pos), 10);
            }
            //canvas.DrawCircle(new PointF(EntityList, 100), 30);
        }
    }
}
