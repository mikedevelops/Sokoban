using System.Collections.Generic;
using Level.Tile;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Level
{   
    [CreateAssetMenu(fileName = "Level", menuName = "Level", order = 1)]
    public class LevelData : SerializedScriptableObject
    {
        public List<List<int>> tiles;
        public List<List<int>> entities;

        public void Init()
        {
            tiles = new List<List<int>>(0);
            entities = new List<List<int>>(0);
        }

        public void AddTile(TileType type, Vector2Int position)
        {
            List<int> tile = new List<int>
            {
                (int) type,
                position.x,
                position.y
            };
                
            tiles.Add(tile);
        }

        public void AddEntity(EntityType type, Vector2Int position)
        {
            List<int> entity = new List<int>
            {
                (int) type,
                position.x,
                position.y
            };
                
            entities.Add(entity);
        }
    }
}
