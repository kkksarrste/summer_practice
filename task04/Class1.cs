namespace task04
{
    public interface ISpaceship
    {
        void MoveForward();
        void Rotate(int angle);
        void Fire();
        int Speed { get; }
        int FirePower { get; }
    }

    public class Cruiser : ISpaceship
    {
        public int Speed { get; } = 50;
        public int FirePower { get; } = 100;
        private int _currentAngle;
        private int _distanceMoved;
        private int _shotsFired;

        public void MoveForward()
        {
            _distanceMoved += Speed;
        }

        public void Rotate(int angle)
        {
            _currentAngle = (_currentAngle + angle) % 360;
            if (_currentAngle < 0) _currentAngle += 360;
        }

        public void Fire()
        {
            _shotsFired++;
        }
    }

    public class Fighter : ISpaceship
    {
        public int Speed { get; } = 100;
        public int FirePower { get; } = 50;
        private int _currentAngle;
        private int _distanceMoved;
        private int _shotsFired;

        public void MoveForward()
        {
            _distanceMoved += Speed;
        }

        public void Rotate(int angle)
        {
            _currentAngle = (_currentAngle + angle) % 360;
            if (_currentAngle < 0) _currentAngle += 360;
        }

        public void Fire()
        {
            _shotsFired++;
        }
    }
}
