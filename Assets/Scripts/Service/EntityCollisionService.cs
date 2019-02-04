using UnityEngine;

namespace Service
{
    public class EntityCollisionService: MonoBehaviour
    {
        public delegate void EntityCollision(GameObject hit);
        public event EntityCollision OnCollision;
        public LayerMask interactionMask;

        private void OnCollisionEnter(Collision other)
        {           
            bool interaction = interactionMask == (interactionMask | (1 << other.gameObject.layer));

            if (!interaction)
                return;

            OnCollision?.Invoke(other.gameObject);
        }
    }
}