using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour {

    [SerializeField]
    string nextLevelName;

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            StartCoroutine(NextLevel());
            DontDestroyOnLoad(col.gameObject);
            DontDestroyOnLoad(FindObjectOfType<PlayerCamera>());
        }
    }

    IEnumerator NextLevel() {
        float fadingTime = FindObjectOfType<FadeEffect>().BeginFade(1);
        yield return new WaitForSeconds(fadingTime);
        SceneManager.LoadScene(nextLevelName);
        StopAllCoroutines();
    }
}
