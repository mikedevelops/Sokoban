using UnityEngine;

namespace Level.Tile
{
    public class BlockGoalTile: AbstractTile, IFulfillableTile
    {
        public LayerMask interactionMask;

        private bool _active;
        
        public override TileType GetTileType()
        {
            return TileType.BlockGoal;
        }

        public bool IsFulfilled()
        {
            return Physics.Raycast(transform.position, Vector3.up, 1f, interactionMask);
        }
    }
}