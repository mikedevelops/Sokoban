using UnityEngine;
using Utils;

namespace Service
{
    public class EntityCollisionService: MonoBehaviour
    {
        public delegate void EntityCollision(GameObject hit);
        public event EntityCollision OnCollision;
        public LayerMask interactionMask;

        private void OnCollisionEnter(Collision other)
        {
            bool interaction = LayerUtils.IsLayer(other.gameObject.layer, interactionMask);

            if (!interaction)
                return;

            OnCollision?.Invoke(other.gameObject);
        }
    }
}