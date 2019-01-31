using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Level.Tile;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Level
{
    public class LevelGrid: MonoBehaviour
    {
        [Space] public LevelData level;
        
        [Space]
        public Row rowPrefab;
        public Tile.Tile defaultTilePrefab;
        public PlayerGoalTile playerGoalTilePrefab;
        
        
        private List<Row> _rows;

        private void Awake()
        {
            Generate();
        }

        [Button]
        public void Generate()
        {
            GenerateLevelFromData(level);
        }

        private Row CreateRow(int position)
        {
            Row row = Instantiate(rowPrefab, transform);
            row.SetPosition(position);

            return row;
        }

        private AbstractTile CreateTile(Vector2Int position, Transform row, TileType type)
        {
            AbstractTile tilePrefab = defaultTilePrefab;

            switch (type)
            {
                case TileType.PlayerGoal:
                    tilePrefab = playerGoalTilePrefab;
                    break;
            }
            
            AbstractTile tile = Instantiate(tilePrefab, row);
            tile.SetCartesianPosition(position);
            tile.SetWorldPosition(position);
            
            return tile;
        }

        [Button]
        public void Clear()
        {
            if (transform.childCount > 0)
            {
                for (int c = 0; c < transform.childCount; c++)
                {
                    DestroyImmediate(transform.GetChild(c).gameObject);
                }
            }
            
            _rows = new List<Row>(0);
        }

        [CanBeNull]
        public AbstractTile GetTile(Vector2Int position)
        {
            try
            {
                return _rows[position.y].GetTile(position.x);
            }
            catch (Exception exception)
            {
                Debug.LogWarning(exception);
                
                return null;
            }
        }

        public AbstractTile[] GetTiles()
        {
            List<AbstractTile> collection = new List<AbstractTile>(0);

            foreach (Row gridRow in _rows)
            {
                collection.AddRange(gridRow.GetTiles());
            }

            return collection.ToArray();
        }

        [Button]
        public void GenerateLevelFromData(LevelData levelData)
        {
            Clear();

            for (int y = 0; y < levelData.tiles.GetLength(1); y++)
            {
                if (_rows.ElementAtOrDefault(y) == null)
                    _rows.Add(CreateRow(y));

                for (int x = 0; x < levelData.tiles.GetLength(0); x++)
                {
                    TileType type = levelData.tiles[x, y];
                    
                    if (type == TileType.None)
                        continue;
                    
                    _rows[y].AddTile(CreateTile(new Vector2Int(x, y), _rows[y].transform, type));
                }
            }
        }
    }
}