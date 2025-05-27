namespace ReactorSim.Models
{
  public class ControlRod
  {
    public float y_pos { get; set; }
    public float movement { get; set; }
    public ControlRod()
    {
      y_pos = 0;
      movement = 0;
    }
  }
}
