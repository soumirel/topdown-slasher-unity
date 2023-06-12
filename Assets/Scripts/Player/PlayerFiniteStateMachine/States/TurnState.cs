using Animations;
using Components;

namespace Player.PlayerFiniteStateMachine.States
{
    public class TurnState : PlayerState
    {
        private Hands _hands;
        private Movement _movement;
        private AnyStateAnimator _anyStateAnimator;
        
        public TurnState(Player player, PlayerStateMachine stateMachine, string animationTransitionParam)
            : base(player, stateMachine, animationTransitionParam)
        {
            _movement = player.Movement;
            _hands = player.Hands;
            _anyStateAnimator = player.AnyStateAnimator;
        }
        
        public override void Enter()
        {
            base.Enter();
            player.IsTurning = true;
            player.FacingDirection *= -1;
            _hands.Turn();
            _anyStateAnimator.OnAnimationFinished += FinishTurn;
        }

        public override void Exit()
        {
            _anyStateAnimator.OnAnimationFinished -= FinishTurn;
            base.Exit();
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            _hands.ChangePosition(player.InputHandler.SightDirection);
        }
        
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            _movement.Move(inputHandler.MovementDirection);
        }


        protected override void CheckTransitions()
        {
            if (player.IsTurning) return;
            
            if (inputHandler.IsMoving)
            {
                SwitchState(PlayerStateType.Move);
            }
            else
            {
                SwitchState(PlayerStateType.Idle);
            }
        }


        private void FinishTurn()
        {
            player.IsTurning = false;
            
            player.SpriteRenderer.flipX = !player.SpriteRenderer.flipX;
        }
    }
}