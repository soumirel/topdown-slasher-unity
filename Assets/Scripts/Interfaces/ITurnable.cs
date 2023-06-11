namespace Interfaces
{
    public interface ITurnable
    {
        public bool IsTurning { get; }
        public int FacingDirection { get; }
        public float TurnSpeedSeconds { get; set; }
        
        void StartTurn();
        void FinishTurn();
    }
}