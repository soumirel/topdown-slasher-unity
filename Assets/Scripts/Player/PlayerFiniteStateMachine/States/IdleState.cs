using ComponentSystem;

namespace Player.PlayerFiniteStateMachine.States
{
    public class IdleState : PlayerState
    {
        public IdleState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam) 
            : base(player, stateMachine, hashedAnimatorParam) {}

        public override void Enter()
        {
            _movement.SetVelocityZero();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (player.InputHandler.AttackInput && player.Weapon.IsReady)
            {
                stateMachine.SwitchState(PlayerStateType.Attack);
            }
            
            if (player.InputHandler.MovementDirection.sqrMagnitude != 0)
            {
                stateMachine.SwitchState(PlayerStateType.Move);
            }

            player.Animator.SetFloat(player.SightXParam, player.InputHandler.SightDirection.x);
            player.Animator.SetFloat(player.SightYParam, player.InputHandler.SightDirection.y);
            
            player.Weapon.Rotate(player.InputHandler.WorldSightPosition);
        }
    }
}