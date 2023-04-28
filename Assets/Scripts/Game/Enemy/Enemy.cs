using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _blood;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(_blood, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }


}
