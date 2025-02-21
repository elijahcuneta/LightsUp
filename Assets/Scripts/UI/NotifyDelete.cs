using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyDelete : MonoBehaviour {

    [SerializeField]
    GameObject[] objectsWillNotify;

	void Update () {
        foreach(GameObject g in objectsWillNotify) {
            if (g == null || g.transform.parent != null) {
                Destroy(gameObject);
            }
        }
	}
}
