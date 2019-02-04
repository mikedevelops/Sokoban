using Level;
using Level.Tile;
using Managers.Block;
using Managers.Player;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Service
{
    public class LevelEditor : SerializedMonoBehaviour
    {
        public LevelGrid grid;
        
        [SerializeField]
        public string levelName = "new_level";

        [Button]
        public void SaveLevel()
        {
            LevelData levelData = ScriptableObject.CreateInstance<LevelData>();
            levelData.Init();
            
            for (int i = 0; i < grid.transform.childCount; i++)
            {
                AbstractTile tile = grid.transform.GetChild(i).GetComponent<AbstractTile>();
                PlayerMovementManager player = grid.transform.GetChild(i).GetComponent<PlayerMovementManager>();
                BlockMovementManager block = grid.transform.GetChild(i).GetComponent<BlockMovementManager>();

                if (tile != null)
                {
                    levelData.AddTile(tile.GetTileType(), tile.GetCartesianPosition());
                }

                if (player != null)
                {
                    levelData.AddEntity(EntityType.Player, player.GetCartesianPosition());
                }

                if (block != null)
                {
                    levelData.AddEntity(EntityType.Block, block.GetCartesianPosition());   
                }
            }
            
            AssetDatabase.CreateAsset(levelData, $"Assets/Levels/{levelName}.asset");
        }
    }
}
