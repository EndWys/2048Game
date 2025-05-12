using Assets._Project.Scripts.Extensions;
using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject;
using UnityEngine;

public class OneSideWall : MonoBehaviour
{
    [SerializeField] private LayerMask _mainCubeLayer;
    private void OnTriggerExit(Collider other)
    {
        if (_mainCubeLayer.Contains(other.gameObject.layer))
        {
            other.gameObject.layer = LayerMask.NameToLayer(Cube.CUBE_LAYER);
        }
    }
}
