using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _launchForce = 10f;

    public void Launch()
    {
        _rb.isKinematic = false;
        _rb.AddForce(Vector3.forward * _launchForce, ForceMode.Impulse);
    }
}
