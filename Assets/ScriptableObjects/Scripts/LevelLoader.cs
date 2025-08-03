using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelLoader", menuName = "ScriptableObjects/LevelLoader")]
public class LevelLoader : ScriptableObject
{
    public int level;

    [SerializeField] private GridConfig _gridConfig;
    public void LoadLevel()
    {
        _gridConfig.width = _gridConfig.levels[level].width;
        _gridConfig.height = _gridConfig.levels[level].height;
        _gridConfig.layoutString = _gridConfig.levels[level].stringLayout;
        SceneManager.LoadSceneAsync(1);
    }
}
