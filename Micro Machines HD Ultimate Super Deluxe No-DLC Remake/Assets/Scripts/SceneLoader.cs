using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneLoader : MonoBehaviour
{
    public bool UnloadOldScene;
    private Scene _activeScene;

    private void Awake() => _activeScene = SceneManager.GetActiveScene();

    public void SetUnloadOldScene(bool enable) => UnloadOldScene = enable;

    public void LoadScene(int sceneBuildIndex)
    {
        if (UnloadOldScene)
        {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            UnloadScene(_activeScene.buildIndex);
        }
        else
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        _activeScene = SceneManager.GetActiveScene();
    }

    public void LoadSceneAsync(int sceneBuildIndex)
    {
        if (UnloadOldScene)
        {
            SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
            UnloadScene(_activeScene.buildIndex);
        }
        else
            SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);

        _activeScene = SceneManager.GetActiveScene();
    }

    public void LoadSceneAdditive(int sceneBuildIndex)
    {
        if (UnloadOldScene)
        {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Additive);
            UnloadScene(_activeScene.buildIndex);
        }
        else
            SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);

        _activeScene = SceneManager.GetActiveScene();
    }

    public void LoadSceneAsyncAdditive(int sceneBuildIndex)
    {
        if (UnloadOldScene)
        {
            SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive); ;
            UnloadScene(_activeScene.buildIndex);
        }
        else
            SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);

        _activeScene = SceneManager.GetActiveScene();
    }

    public void UnloadScene(int sceneBuildIndex) => SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(sceneBuildIndex));
}