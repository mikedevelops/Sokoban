using System.Collections.Generic;
using JetBrains.Annotations;
using Level.Tile;
using UnityEngine;

namespace Level
{
    public class Row: MonoBehaviour
    {
        private int _position;
        private List<AbstractTile> _tiles;

        private void Awake()
        {
            _tiles = new List<AbstractTile>(0);
        }

        public void AddTile(AbstractTile tile)
        {
            // If we're in the editor and `Awake` is not invoked
            if (_tiles == null)
                _tiles = new List<AbstractTile>(0);
            
            _tiles.Add(tile);
        }

        public void SetPosition(int position)
        {
            _position = position;
        }

        [CanBeNull]
        public AbstractTile GetTile(int position)
        {
            return _tiles.Find(tile => tile.GetCartesianPosition().x == position);
        }

        public IEnumerable<AbstractTile> GetTiles()
        {
            return _tiles;
        }
    }
}