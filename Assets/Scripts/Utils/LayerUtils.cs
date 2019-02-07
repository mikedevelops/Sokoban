using UnityEngine;

namespace Utils
{
    public static class LayerUtils
    {
        public static bool IsLayer(int layer, LayerMask mask)
        {
            return mask == (mask | (1 << layer));
        }
    }
}