using Level.Tile;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Level
{    
    [CreateAssetMenu(fileName = "Level", menuName = "Level", order = 1)]
    public class LevelData : SerializedScriptableObject
    {
        [TableMatrix()]
        public TileType[,] tiles;
    }
}
