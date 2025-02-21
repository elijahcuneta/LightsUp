using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaboxyTarget : MonoBehaviour {

    Jaboxy jaboxy;

	void Start () {
        jaboxy = transform.root.GetComponent<Jaboxy>();
	}

    void OnTriggerEnter(Collider other) {
        
    }
}
