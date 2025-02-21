using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaboxyHand : MonoBehaviour {

    public int damage;

    AnimatorStateInfo enemStateInfo;
    Animator enemAnim;

    bool hit;

    float timer;

    void Awake() {
        enemAnim = transform.root.GetComponentInChildren<Animator>();
    }
    void Update() {
        enemStateInfo = enemAnim.GetCurrentAnimatorStateInfo(0);

        if (hit) {
            timer += Time.deltaTime;
            if (timer >= 3) {
                hit = false;
                timer = 0;
            }
        }
    }
    void OnTriggerStay(Collider col) {
        if (col.tag == "Player" && enemStateInfo.IsTag("Attack") && !hit) {
            col.GetComponent<Rigidbody>().AddForce(transform.forward * 150f);
            col.GetComponent<PlayerHealth>().TakeDamage(damage);
            hit = true;
        }
    }
}
