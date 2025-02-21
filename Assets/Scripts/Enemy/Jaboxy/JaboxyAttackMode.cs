using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaboxyAttackMode : MonoBehaviour {

    //attackMode = name of the gameobject
    string attackMode;

    Jaboxy jaboxy;

	void Start () {
        jaboxy = transform.parent.parent.GetComponent<Jaboxy>();
        attackMode = gameObject.name;
	}

    void OnTriggerEnter(Collider col) {
        if(col.tag == "Player" && jaboxy != null) {
            jaboxy.Attack(attackMode);
        }
    }
}
