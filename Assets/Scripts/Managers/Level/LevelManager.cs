using System;
using System.Collections.Generic;
using System.Linq;
using Level.Tile;
using Sirenix.Utilities;
using UnityEngine;

namespace Managers.Level
{
    public class LevelManager: MonoBehaviour
    {
        public static LevelManager Instance;
        public global::Level.Level level;

        private List<IFulfillableTile> _fulfillableTiles;

        private void Awake()
        {
            if (Instance != null)
                throw new Exception("Multiple instances of the LevelManager");

            Instance = this;
            
            GetFulfillableEntities();
            CheckWinCondition();
        }

        private void GetFulfillableEntities()
        {
            AbstractTile[] tiles = level.GetTiles();
            List<IFulfillableTile> fulfillableTiles = new List<IFulfillableTile>(0);

            foreach (AbstractTile tile in tiles)
            {
                IFulfillableTile ft = tile as IFulfillableTile;
                
                if (ft != null)
                    fulfillableTiles.Add(ft);
            }

            _fulfillableTiles = fulfillableTiles;
        }

        public void CheckWinCondition()
        {
            if (_fulfillableTiles == null)
                return;

            // Check active state of fulfillable tiles
            _fulfillableTiles.FilterCast<PlayerGoalTile>().ForEach(tile => tile.GetIsActive());
            
            IFulfillableTile[] fulfilledTiles = _fulfillableTiles.Where(tile => tile.IsFulfilled()).ToArray();
            
            if (fulfilledTiles.Length == _fulfillableTiles.Count)
                Debug.Log("Level Complete");
        }

        public bool IsTileActive(AbstractTile tile)
        {
            PlayerGoalTile playerGoalTile = tile as PlayerGoalTile;

            if (playerGoalTile != null)
                return BlockGoalsFulfilled();
               
            return true;
        }

        private bool BlockGoalsFulfilled()
        {            
            BlockGoalTile[] blockGoals = _fulfillableTiles.FilterCast<BlockGoalTile>().ToArray();

            if (blockGoals.Length == 0)
                return true;

            BlockGoalTile[] blockGoalsFulfilled = blockGoals.Where(tile => tile.IsFulfilled()).ToArray();

            return blockGoalsFulfilled.Length == blockGoals.Length;
        }
    }
}