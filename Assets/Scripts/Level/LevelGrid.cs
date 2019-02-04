using System;
using System.Collections.Generic;
using System.Linq;
using Agents;
using JetBrains.Annotations;
using Level.Tile;
using Managers;
using Managers.Block;
using Managers.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Level
{
    public enum EntityType
    {
        Player,
        Block
    }
    
    public class LevelGrid: MonoBehaviour
    {
        [Space] 
        public LevelData level;
        
        [Space]
        public Tile.Tile defaultTilePrefab;
        public PlayerGoalTile playerGoalTilePrefab;
        public BlockGoalTile blockGoalTilePrefab;
        public PlayerMovementManager playerPrefab;
        public BlockMovementManager blockPrefab;

        private List<AbstractTile> _tiles;
        private string _test;

        private void Awake()
        {
            Generate();
        }

        [Button]
        public void Generate()
        {
            GenerateLevelFromData(level);
            _test = "done!";
        }

        [Button]
        public void Clear()
        {
			List<Transform> children = transform.Cast<Transform>().ToList();

            foreach(Transform child in children)
				DestroyImmediate(child.gameObject);
            
            _tiles = new List<AbstractTile>(0);
        }

        [CanBeNull]
        public AbstractTile GetTile(Vector2Int position)
        {
            return _tiles.Find(tile => tile.GetCartesianPosition() == position);
        }

        public AbstractTile[] GetTiles()
        {
            return _tiles.ToArray();
        }

        [Button]
        public void GenerateLevelFromData(LevelData levelData)
        {
            if (levelData == null)
            {
                Debug.LogError("Add a level file to the inspector");
                return;
            }
            
            Clear();
            
            levelData.tiles?.ForEach(serializedTile =>
            {
                TileType tileType = (TileType) serializedTile[0];

                switch (tileType)
                {
                    case TileType.None:
                        break;
                    case TileType.Default:
                        CreateTile(defaultTilePrefab, new Vector2Int(serializedTile[1], serializedTile[2]));                       
                        break;
                    case TileType.PlayerGoal:
                        CreateTile(playerGoalTilePrefab, new Vector2Int(serializedTile[1], serializedTile[2]));
                        break;
                    case TileType.BlockGoal:
                        CreateTile(blockGoalTilePrefab, new Vector2Int(serializedTile[1], serializedTile[2]));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
            
            levelData.entities?.ForEach(serializedEntity =>
            {
                EntityType entityType = (EntityType) serializedEntity[0];

                switch (entityType)
                {
                    case EntityType.Player:
                        CreateEntity(playerPrefab, new Vector2Int(serializedEntity[1], serializedEntity[2]));
                        break;
                    case EntityType.Block:
                        CreateEntity(blockPrefab, new Vector2Int(serializedEntity[1], serializedEntity[2]));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
        }

        private void CreateTile(AbstractTile prefab, Vector2Int position)
        {
            AbstractTile tile = Instantiate(prefab, transform);
            tile.SetWorldPosition(position);
            _tiles.Add(tile);
        }

        private void CreateEntity(AbstractMovementManager prefab, Vector2Int position)
        {
            AbstractMovementManager entity = Instantiate(prefab, transform);
            IEntityWithOffset entityWithOffset = entity as IEntityWithOffset;

            entity.grid = this;
            
            if (entityWithOffset != null)
            {
                entityWithOffset.SetPosition(new Vector3(position.x, 0, position.y));
                
                return;
            }

            entity.transform.position = new Vector3(position.x, 0, position.y);
        }
    }
}