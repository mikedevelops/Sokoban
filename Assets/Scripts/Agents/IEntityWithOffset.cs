using UnityEngine;

namespace Agents
{
    public interface IEntityWithOffset
    {
        Vector3 GetOffset();

        void SetPosition(Vector3 position);
    }
}