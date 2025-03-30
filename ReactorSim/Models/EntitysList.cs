namespace ReactorSim.Models
{
    public class EntitysList
    {
        private static EntitysList _instance;

        public static EntitysList Instance
        {
            get { return _instance; } 
        }

        public List<Neutron> neutronList = new List<Neutron>();

        public EntitysList() { }
    }
}
