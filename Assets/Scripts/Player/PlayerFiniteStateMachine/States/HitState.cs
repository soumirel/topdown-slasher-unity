
namespace Player.PlayerFiniteStateMachine.States
{
    public class HitState : PlayerState
    {
        public HitState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam)
            : base(player, stateMachine, hashedAnimatorParam)
        {
        }
    }
}