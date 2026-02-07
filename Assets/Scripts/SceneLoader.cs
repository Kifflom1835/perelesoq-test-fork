using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoSingleton<SceneLoader>
{

    bool isReloading = false;
    bool isFirstLoad = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);

        if(isFirstLoad)
        {
            StartCoroutine(LoadAdditionlScenes());
            isFirstLoad = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(ReloadAllActiveScenes());
        }
    }

    private IEnumerator ReloadAllActiveScenes()
    {
        isReloading = true;
        // Выгружаем все сцены
        int sceneCount = SceneManager.loadedSceneCount;

        for (int i=0; i <sceneCount; i++)
        {
            yield return SceneManager.UnloadSceneAsync(i);
        }
        
        // Загружаем основную сцену
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        
        yield return StartCoroutine(LoadAdditionlScenes());
        isReloading = false;
    }

    private IEnumerator LoadAdditionlScenes()
    {
        // Загружаем дополнительные сцены
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            yield return SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
        }
    }
}
