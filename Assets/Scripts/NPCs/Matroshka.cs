using UnityEngine;

public class Matroshka : MonoBehaviour {

    Scripts myScript;

    void Start() {
        myScript = GetComponent<Scripts>();
    }

    //temp code
    void Update() {
        if (GameObject.Find("Toy Blocks - Breakable") == null) {
            myScript.indexOfScript = 1;
        }
    }
}
