using UnityEngine;

namespace Level.Tile
{
    public class PlayerGoalTile: AbstractTile, IFulfillableTile
    {
        public LayerMask interactionMask;
        
        private bool _fulfilled;

        public bool IsFulfilled()
        {
            return Physics.Raycast(transform.position, Vector3.up, 1f, interactionMask);
        }
    }
}