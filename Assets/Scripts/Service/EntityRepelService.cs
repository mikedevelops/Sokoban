using Instructions;
using JetBrains.Annotations;
using Managers;
using Managers.Block;
using UnityEngine;

namespace Service
{
    public class EntityRepelService: MonoBehaviour
    {
        public delegate void EntityRepelled(Vector2Int direction);
        public event EntityRepelled OnRepel;
        public SheepMovementManager movementManager;
        public LayerMask interactionMask;

        private void Update()
        {
            if (OnRepel == null)
                return;
            
            AbstractMovementManager other = Detect();

            if (other != null)
            {
                MovementInstruction instruction = new MovementInstruction(GetOtherDirection(other));
                
                // TODO: This is broken
                
                if (movementManager.IsValidMove(instruction))
                    OnRepel?.Invoke(instruction.Direction);
            }
        }
        
        [CanBeNull]
        private AbstractMovementManager Detect()
        {
            Vector3 position = transform.position;
            float castLength = 1f;
            RaycastHit northHit;
            RaycastHit eastHit;
            RaycastHit southHit;
            RaycastHit westHit;
            
            Debug.DrawRay(position, Vector3.forward * castLength, Color.red);
            Debug.DrawRay(position, Vector3.right * castLength, Color.red);
            Debug.DrawRay(position, Vector3.back * castLength, Color.red);
            Debug.DrawRay(position, Vector3.left * castLength, Color.red);
            
            Physics.Raycast(position, Vector3.forward, out westHit, castLength, interactionMask);

            if (westHit.collider != null)
                return westHit.collider.GetComponent<AbstractMovementManager>();
            
            Physics.Raycast(position, Vector3.back, out eastHit, castLength, interactionMask);
            
            if (eastHit.collider != null)
                return eastHit.collider.GetComponent<AbstractMovementManager>();
            
            Physics.Raycast(position, Vector3.right, out northHit, castLength, interactionMask);
            
            if (northHit.collider != null)
                return northHit.collider.GetComponent<AbstractMovementManager>();            
            
            Physics.Raycast(position, Vector3.left, out southHit, castLength, interactionMask);
                        
            if (southHit.collider != null)
                return southHit.collider.GetComponent<AbstractMovementManager>();

            return null;
        }

        private Vector2Int GetOtherDirection(AbstractMovementManager other)
        {
            Vector3 heading = transform.position - other.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            
            return new Vector2Int(Mathf.RoundToInt(direction.x), Mathf.RoundToInt(direction.z));
        }
    }
}