namespace GameLogic.States
{
    internal class FinalBeforeGameOverState : StateAdapter
    {
        public FinalBeforeGameOverState(GameData gameData) : base(gameData)
        {
            
        }

        public override bool HaveMafiaWon()
        {
            return gameData.PlayerManager.haveMafiaWon();
        }
    }
}