using Managers.Level;
using UnityEngine;

namespace Level.Tile
{
    public class PlayerGoalTile: AbstractTile, IFulfillableTile
    {
        public LayerMask interactionMask;
        
        private bool _fulfilled;
        private bool _active;

        public bool IsFulfilled()
        {
            return Physics.Raycast(transform.position, Vector3.up, 1f, interactionMask);
        }

        public override TileType GetTileType()
        {
            return TileType.PlayerGoal;
        }

        public void GetIsActive()
        {            
            bool active = LevelManager.Instance.IsTileActive(this);
            
//            gameObject.SetActive(active);
            _active = active;
        }
    }
}