using UnityEngine;

namespace Assets._Project.Scripts.Utilities
{
    public class CachedMonoBehaviour : MonoBehaviour
    {
        private GameObject _cachedGameobject;
        public GameObject CachedGameObject
        {
            get
            {
                if (_cachedGameobject == null)
                    _cachedGameobject = gameObject;

                return _cachedGameobject;
            }
        }

        private Transform _cachedTrasform;
        public Transform CachedTrasform
        {
            get
            {
                if (_cachedTrasform == null)
                    _cachedTrasform = transform;

                return _cachedTrasform;
            }
        }
    }
}