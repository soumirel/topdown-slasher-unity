using UnityEngine;

namespace Player.PlayerFiniteStateMachine.States
{
    public class AimState : PlayerState
    {
        public AimState(PlayerController player, PlayerStateMachine stateMachine, int hashedAnimatorParam)
            : base(player, stateMachine, hashedAnimatorParam)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            movement.SetVelocityZero();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (player.InputHandler.AttackStarted || player.InputHandler.AttackPerformed)
            {
                SwitchState(PlayerStateType.Attack);
            }
        }
    }
}