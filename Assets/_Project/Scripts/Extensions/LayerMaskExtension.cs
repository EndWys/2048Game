using UnityEngine;

namespace Assets._Project.Scripts.Extensions
{
    public static class LayerMaskExtension
    {
        public static bool Contains(this LayerMask layerMask, int index)
        {
            return (layerMask & (1 << index)) != 0;
        }
    }
}