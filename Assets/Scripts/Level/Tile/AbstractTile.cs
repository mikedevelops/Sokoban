using UnityEngine;

namespace Level.Tile
{
    public abstract class AbstractTile: MonoBehaviour
    {
        public TileType type;
        protected readonly Vector3 Offset = new Vector3(0, -0.25f, 0);
        
        private Vector2Int _cartesianPosition;
        
        public Vector2Int GetCartesianPosition()
        {
            return _cartesianPosition;
        }
        
        public void SetCartesianPosition(Vector2Int position)
        {
            _cartesianPosition = position;
        }
        
        public void SetWorldPosition(Vector2Int position)
        {
            Vector3 offsetPosition = new Vector3(position.x, 0, position.y) + GetOffset();
            transform.position = new Vector3(offsetPosition.x, offsetPosition.y, offsetPosition.z);
        }

        public TileType GetTileType()
        {
            return type;
        }
        
        private  Vector3 GetOffset()
        {
            return Offset;
        }
    }
}