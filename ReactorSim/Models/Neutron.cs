namespace ReactorSim.Models
{
    public class Neutron
    {
        public float x_pos { get; set; }
        public float y_pos { get; set; }
        public float speed { get; set; }
        public float direction { get; set; }
        public bool isFast { get; set; }

        public Neutron(float _x_pos, float _y_pos, float _speed, float _direction, bool _isFast) 
        {
            x_pos = _x_pos;
            y_pos = _y_pos;
            speed = _speed;
            direction = _direction;
            isFast = _isFast;

            count++;
        }

        public static int count = 0;
    }
}
