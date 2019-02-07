using Level;
using Level.Tile;
using Managers.Block;
using Managers.Player;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Service
{
    public class LevelEditor : SerializedMonoBehaviour
    {
        public Level.Level level;
        
        [SerializeField]
        public string levelName = "new_level";

        [Button]
        public void SaveLevel()
        {
            LevelData levelData = ScriptableObject.CreateInstance<LevelData>();
            levelData.Init();
            
            for (int i = 0; i < level.transform.childCount; i++)
            {
                AbstractTile tile = level.transform.GetChild(i).GetComponent<AbstractTile>();
                PlayerMovementManager player = level.transform.GetChild(i).GetComponent<PlayerMovementManager>();
                BlockMovementManager block = level.transform.GetChild(i).GetComponent<BlockMovementManager>();
                SheepMovementManager sheep = level.transform.GetChild(i).GetComponent<SheepMovementManager>();

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

                if (sheep != null)
                {
                    levelData.AddEntity(EntityType.Sheep, sheep.GetCartesianPosition());
                }
            }
            
            AssetDatabase.CreateAsset(levelData, $"Assets/Levels/{SceneManager.GetActiveScene().name}_{levelName}.asset");
        }
    }
}
