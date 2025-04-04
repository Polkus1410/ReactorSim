﻿namespace ReactorSim.Models
{
  public class Cell
  {
    public bool isUranium { get; set; }
    public bool isXenon { get; set; }
    public int waterTemp { get; set; }
    public int xenonCountDowbn { get; set; }

    public Cell(bool _isUranium, bool _isXenon)
    {
      isUranium = _isUranium;
      isXenon = _isXenon;
      waterTemp = 30;
      xenonCountDowbn = 0;
    }
  }
}
