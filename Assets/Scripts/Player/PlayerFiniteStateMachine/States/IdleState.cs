namespace Player.PlayerFiniteStateMachine.States
{
    public class IdleState : PlayerState
    {
        public IdleState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam) 
            : base(player, stateMachine, hashedAnimatorParam) {}

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            player.Animator.SetFloat(player.SightXParam ,player.InputHandler.SightDirection.x);
            player.Animator.SetFloat(player.SightYParam ,player.InputHandler.SightDirection.y);

            if (player.InputHandler.MovementDirection.sqrMagnitude != 0)
            {
                stateMachine.SwitchState(PlayerStateType.Move);
            }

            if (player.InputHandler.AttackInput)
            {
                stateMachine.SwitchState(PlayerStateType.Attack);
            }
        }
    }
}