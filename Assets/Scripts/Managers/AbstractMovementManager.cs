using Instructions;
using Level;
using Level.Tile;
using State.Entity;
using UnityEngine;
using Utils;

namespace Managers
{
    public abstract class AbstractMovementManager: AbstractStateManager, IEntityWithSpeed, IEntityWithSnap
    {
        public float speed;
        public float snapDistance = 0.025f;
        public global::Level.Level grid;
        
        public float GetSpeed()
        {
            return speed;
        }

        public float GetSnapDistance()
        {
            return snapDistance;
        }

        public virtual bool IsValidMove(MovementInstruction instruction, int? entityLayer = null)
        {
            Vector2Int target = GetCartesianPosition() + instruction.Direction;
            AbstractTile tile = grid.GetTile(target);
            
            int layer = entityLayer ?? gameObject.layer;

            if (tile == null)
                return false;
            
            Debug.Log(tile.transform.position);

            return LayerUtils.IsLayer(layer, tile.entityWhiteList);
        }

        public Vector2Int GetCartesianPosition()
        {
            Vector3 position = transform.position;
            
            return new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.z));
        }
    }
}