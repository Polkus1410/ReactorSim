namespace ReactorSim.Models
{
  public class ControlRod
  {
    public float x_pos { get; set; }
    public float y_pos { get; set; }
    public float movement { get; set; }
    public ControlRod(float _x_pos)
    {
      x_pos = _x_pos;
      y_pos = 0;
      movement = 0;
    }
  }
}
