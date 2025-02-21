using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaboxy : MonoBehaviour {

    EnemyHealth jaboxyHealth;
    Animator jaboxyAnim;

    bool spawnMode, dead;

	void Start () {
        jaboxyHealth = GetComponentInChildren<EnemyHealth>();
        jaboxyAnim = GetComponentInChildren<Animator>();
    }
	
	void Update () {
        Animating();
	}

    void Animating() {
        jaboxyAnim.SetBool("Spawn Mode", spawnMode);
        if (jaboxyHealth.isDead && !dead) {
            jaboxyAnim.SetTrigger("Death");
            GetComponent<BoxCollider>().enabled = false;
            dead = true;
        }
    }

    public void Attack(string name) {
        jaboxyAnim.SetTrigger(name);
    }

    void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            spawnMode = true;
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Player") {
            spawnMode = false;
        }
    }

}
