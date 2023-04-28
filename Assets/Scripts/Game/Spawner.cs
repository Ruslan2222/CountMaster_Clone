using UnityEngine;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabUnity;
    [SerializeField] private Transform _unitContainer;
    [Space]
    [Range(0, 1)] [SerializeField] private float _distance, _radius;

    private void Spawn(int count, GameObject prefab, Transform transform)
    {
        if (count > 1)
        {
            for (int i = 0; i < count; i++)
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
            }

            DistanceSpawn(transform);
        }

    }

    public void SpawnMan(int count, GameObject prefab, Transform transform) => Spawn(count, prefab, transform);

    private void DistanceSpawn(Transform transform)
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            var x = _distance * Mathf.Sqrt(i) * Mathf.Cos(i * _radius);
            var z = _distance * Mathf.Sqrt(i) * Mathf.Sin(i * _radius);

            var NewPos = new Vector3(x, -0.55f, z);

            transform.GetChild(i).DOLocalMove(NewPos, 1f).SetEase(Ease.OutBack);
        }
    }

    public void Format(Transform transform) => DistanceSpawn(transform);

    private void SpawnUnit(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(_prefabUnity, _unitContainer.position, Quaternion.identity, _unitContainer);
        }

        DistanceSpawn(_unitContainer);
    }

    public void UnitSpawn(int count) => SpawnUnit(count);

}
