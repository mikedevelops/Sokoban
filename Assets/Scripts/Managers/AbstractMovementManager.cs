using Instructions;
using Level;
using State.Entity;
using UnityEngine;

namespace Managers
{
    public abstract class AbstractMovementManager: AbstractStateManager, IEntityWithSpeed, IEntityWithSnap
    {
        public float speed;
        public float snapDistance = 0.025f;
        public LevelGrid grid;
        
        public float GetSpeed()
        {
            return speed;
        }

        public float GetSnapDistance()
        {
            return snapDistance;
        }

        public virtual bool IsValidMove(MovementInstruction instruction)
        {
            Vector2Int target = GetCartesianPosition() + instruction.Direction;
            
            return grid.GetTile(target) != null;
        }

        public Vector2Int GetCartesianPosition()
        {
            Vector3 position = transform.position;
            
            return new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.z));
        }
    }
}