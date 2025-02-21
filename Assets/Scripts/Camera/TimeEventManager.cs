using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEventManager : MonoBehaviour {

    [SerializeField]
    float slowDownValue = 0.05f, slowDownLength = 2;

    private static float originalFixedDeltaTime;

    void Start() {
        originalFixedDeltaTime = Time.fixedDeltaTime;
    }

    IEnumerator NormalMotion() {
        while(Time.timeScale < 0.9f) {
            Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
            Time.fixedDeltaTime = Time.timeScale * originalFixedDeltaTime;
            yield return null;
        }
        Time.timeScale = 1;
        Time.fixedDeltaTime = originalFixedDeltaTime;
        StopAllCoroutines();
    }

    public void SlowMotion() {
       Time.timeScale = slowDownValue;
       Time.fixedDeltaTime = Time.timeScale * originalFixedDeltaTime;
    }

}