using UnityEngine;

namespace Instructions
{
    public class MovementInstruction
    {
        public Vector2Int Direction;

        public MovementInstruction(Vector2Int direction)
        {
            Direction = direction;
        }
    }
}