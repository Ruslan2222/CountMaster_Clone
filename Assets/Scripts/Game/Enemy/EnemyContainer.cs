using UnityEngine;
using TMPro;
using DG.Tweening;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private GameObject _prefabMan;
    [SerializeField] private TextMeshPro _counterText;
    [SerializeField] private GameObject _enemyZone;

    [Range(0, 1)] [SerializeField] private float _distance, _radius;

    private Transform _playerTransform;
    private bool _isAttacking;

    private void Start()
    {
        Spawn();
    }

    private void Update()
    {

        #region Attack
        if (_isAttacking && transform.childCount > 1)
        {
            var enemyDirection = _playerTransform.position - transform.position;

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, Quaternion.LookRotation(enemyDirection, Vector3.up),
                    Time.deltaTime * 3f);

                if (_playerTransform.childCount > 1)
                {
                    var distance = _playerTransform.GetChild(1).position - transform.GetChild(i).position;

                    if (distance.magnitude < 1.5f)
                    {
                        transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position,
                            _playerTransform.GetChild(1).position, Time.deltaTime * 2f);
                    }
                }

            }
            _counterText.text = $"{transform.childCount}";
        }
        else
        {
            _isAttacking = false;
            if (transform.childCount <= 1)
            {
                Destroy(_enemyZone);
            }
            _counterText.text = $"{transform.childCount}";
        }
        #endregion

    }

    private void Spawn()
    {
        int random = Random.Range(20, 80);
        Spawner spawn = FindObjectOfType<Spawner>();
        spawn.SpawnMan(random, _prefabMan, transform);
        _counterText.text = $"{transform.childCount}";
    }

    private void Attack(Transform playerTransform)
    {
        _playerTransform = playerTransform;
        _isAttacking = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().SetBool("run", true);
        }

    }

    public void AttackPlayer(Transform playerTransform) => Attack(playerTransform);

}
