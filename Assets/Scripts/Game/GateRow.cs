using UnityEngine;

public class GateRow : MonoBehaviour
{
    [SerializeField] private BoxCollider[] _gateColliders;

    private void ColliderOff()
    {
        for (int i = 0; i < _gateColliders.Length; i++)
        {
            _gateColliders[i].enabled = false;
        }
    }

    public void OffColiders() => ColliderOff();

}
