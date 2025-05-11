using Assets._Project.Scripts.Extensions;
using UnityEngine;

public class OnSideWall : MonoBehaviour
{
    [SerializeField] private LayerMask _mainCubeLayer;
    private void OnTriggerExit(Collider other)
    {
        if (_mainCubeLayer.Contains(other.gameObject.layer))
        {
            other.gameObject.layer = LayerMask.NameToLayer("Cube");
        }
    }
}
