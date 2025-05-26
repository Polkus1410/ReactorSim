namespace ReactorSim.Models
{
  public class EntitysList
  {
    public float cellSpacing = 0;
    public Cell[,] cellMatrix = new Cell[40, 25];

    public List<Neutron> neutronList = new List<Neutron>();

    public ControlRod[] controlRodsArray = new ControlRod[10];

    //temporary value to draw the borders of simulation area
    public RectF simulationBorder = new RectF(0, 0, 0, 0);
    public EntitysList() { }
  }
}