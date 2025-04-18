using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScene;
    [SerializeField]
    private Slider slider;
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadingScene(sceneIndex));
    }

    IEnumerator LoadingScene(int sceneIndex)
    {
        slider.value = 0;
        loadingScene.SetActive(false);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        float progress = 0;
        while (!operation.isDone)
        {
            loadingScene.SetActive(true);
            progress = Mathf.MoveTowards(progress, operation.progress, Time.deltaTime);
            slider.value = progress;
            if(progress >= 0.9f)
            {
                slider.value = 1f;
                operation.allowSceneActivation = true;
            }
            yield return null;
        }

    }
}
