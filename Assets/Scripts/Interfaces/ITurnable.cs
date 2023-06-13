using System;

namespace Interfaces
{
    public interface ITurnable
    {
        int FacingDirection { get; }
        void Turn();
    }
}