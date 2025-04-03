namespace ReactorSim.Models
{
  public class Cell
  {
    public float x_pos { get; set; }
    public float y_pos { get; set; }
    public bool isUranium { get; set; }
    public bool isXenon { get; set; }
    public int waterTemp { get; set; }
    public int xenonCountDowbn { get; set; }

    public Cell(float _x_pos, float _y_pos, bool _isUranium, bool _isXenon)
    {
      x_pos = _x_pos;
      y_pos = _y_pos;
      isUranium = _isUranium;
      isXenon = _isXenon;
      waterTemp = 30;
      xenonCountDowbn = 0;
    }
  }
}
