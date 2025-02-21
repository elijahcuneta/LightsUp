using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public static int sceneIndex;
    public static string sceneName;

    void Start() {
        FindObjectOfType<FadeEffect>().setAlpha(0);
        LevelLoader(sceneName);
    }

    public void LevelLoader(int sceneIndex) {
        StopAllCoroutines();
        StartCoroutine(AsyncLevel(sceneIndex));
    }

    public void LevelLoader(string sceneName) {
        StopAllCoroutines();
        StartCoroutine(AsyncLevel(sceneName));
    }

    IEnumerator AsyncLevel(int sceneIndex) {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
        while (!async.isDone) {
            yield return new WaitForEndOfFrame();
        }
        FindObjectOfType<FadeEffect>().BeginFade(-1);
    }

    IEnumerator AsyncLevel(string sceneName) {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone) {
            yield return new WaitForEndOfFrame();
        }
        FindObjectOfType<FadeEffect>().BeginFade(-1);
    }
}
