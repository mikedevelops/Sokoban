using UnityEngine;

namespace Level.Tile
{
    public abstract class AbstractTile: MonoBehaviour
    {
        protected readonly Vector3 Offset = new Vector3(0, -0.25f, 0);
        
        private Vector2Int _cartesianPosition;

        public Vector2Int GetCartesianPosition()
        {
            return new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z));
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

        public abstract TileType GetTileType();
        
        private  Vector3 GetOffset()
        {
            return Offset;
        }
    }
}