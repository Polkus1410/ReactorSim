namespace ReactorSim.Models
{
    public class Neutron
    {
        public float x_pos { get; set; }
        public float y_pos { get; set; }
        public float velocity { get; set; }
        public float direction { get; set; }
        public float distanceTravelled { get; set; }
        public bool isFast { get; set; }

        public Neutron(float _x_pos, float _y_pos, float _velocity, float _direction, bool _isFast)
        {
            x_pos = _x_pos;
            y_pos = _y_pos;
            velocity = _velocity;
            direction = _direction;
            distanceTravelled = 0;
            isFast = _isFast;
        }
    }
}
