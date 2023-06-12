namespace Animations
{
    public class AnyStateAnimation
    {
        public string TransitionTag { get; private set; }
        public int Priority { get; private set; }

        public AnyStateAnimation(string transitionTag, int priority)
        {
            TransitionTag = transitionTag;
            Priority = priority;
        }
    }
}