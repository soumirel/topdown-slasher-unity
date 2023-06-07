using ComponentSystem;

namespace Player.PlayerFiniteStateMachine.States
{
    public class IdleState : PlayerState
    {
        public IdleState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam) 
            : base(player, stateMachine, hashedAnimatorParam) {}

        public override void Enter()
        {
            base.Enter();
            movement.SetVelocityZero();
        }

        protected override void CheckTransitions()
        {
            base.CheckTransitions();
            if (player.InputHandler.AttackPerformed && combat.IsReady)
            {
                SwitchState(PlayerStateType.Aim);
            }
            
            if (player.InputHandler.IsMoving)
            {
                SwitchState(PlayerStateType.Move);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            player.Animator.SetFloat(player.SightXParam, player.InputHandler.SightDirection.x);
            player.Animator.SetFloat(player.SightYParam, player.InputHandler.SightDirection.y);
            
            //TODO: Поворот для всех оружий сразу
            combat.CurrentWeapon.Rotate(player.InputHandler.WorldSightPosition);
        }
    }
}