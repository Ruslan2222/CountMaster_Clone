using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level Data", order = 52)]
public class LevelData : ScriptableObject
{
    [Header("Level Data")]
    [Space]
    public GameObject _prefabLevel;
    public string typeLevel;
}
