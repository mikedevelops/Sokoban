using System;
using System.Collections.Generic;
using System.Linq;
using Level;
using Level.Tile;
using UnityEngine;

namespace Managers
{
    public class LevelManager: MonoBehaviour
    {
        public static LevelManager Instance;
        public LevelGrid grid;

        private List<IFulfillableTile> _fulfillableTiles;

        private void Awake()
        {
            if (Instance != null)
                throw new Exception("Multiple instances of the LevelManager");

            Instance = this;
        }

        private void Start()
        {
            GetFulfillableEntities();
        }

        private void GetFulfillableEntities()
        {
            AbstractTile[] tiles = grid.GetTiles();
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
            
            IFulfillableTile[] fulfilledTiles = _fulfillableTiles.Where(tile => tile.IsFulfilled()).ToArray();
            
            if (fulfilledTiles.Length == _fulfillableTiles.Count)
                Debug.Log("Level Complete");
        }
    }
}