namespace UI.Panels.GamePlay
{
    public class GamePlayModel
    {
        public float HealthPoint { get; }
        public float PowerPoint { get; }

        public GamePlayModel(float healthPoint, float powerPoint)
        {
            HealthPoint = healthPoint;
            PowerPoint = powerPoint;
        }
    }
}