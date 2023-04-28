using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelData[] _levelDatas;

    public static string levelType;
    private LevelData _currentLevelData;

    private int _level;

    private void Start()
    {
        _level = PlayerPrefs.GetInt("level");
        SpawnLevel(_level);
    }

    private void SpawnLevel(int index)
    {
        GameObject prefabLevel;

        if (index > _levelDatas.Length)
        {
            index = GetRandomLevel();
        }

        _currentLevelData = _levelDatas[index - 1];

        PlayerPrefs.SetInt("level", index);

        prefabLevel = _currentLevelData._prefabLevel;
        levelType = _currentLevelData.typeLevel;

        Instantiate(prefabLevel, new Vector3(0, 0, 21), Quaternion.identity);

    }

    private int GetRandomLevel()
    {
        int index = Random.Range(0, _levelDatas.Length);

        int lastLevel = PlayerPrefs.GetInt("level");
        if (index == lastLevel)
            return GetRandomLevel();
        else
            return index;
    }

}
