using UnityEngine;

namespace Level.Tile
{
    public abstract class AbstractTile: MonoBehaviour
    {
        public LayerMask entityWhiteList;
        protected readonly Vector3 Offset = new Vector3(0, -0.25f, 0);

        public Vector2Int GetCartesianPosition()
        {
            Vector3 position = transform.position;
            return new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.z));
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