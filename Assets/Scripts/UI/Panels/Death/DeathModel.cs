namespace UI.Panels.Death
{
    public class DeathModel
    {
        public int KillCount { get; private set; }

        public DeathModel(int killCount)
        {
            KillCount = killCount;
        }
        public void GetKillCount(int killCount)
        {
            KillCount = killCount;
        } 
        
    }
}