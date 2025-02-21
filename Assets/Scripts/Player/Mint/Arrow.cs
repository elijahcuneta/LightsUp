using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    [SerializeField]
    GameObject[] hitEffects;

    [SerializeField]
    float speed = 5f, lifeTime = 3f;

    void Start() {
        Destroy(gameObject, lifeTime);
    }

    void Update () {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Enemy" && (col.GetComponent<EnemyHealth>() != null || col.GetComponent<FallHealth>() != null) && !col.isTrigger) {
            if (col.GetComponent<EnemyHealth>() != null) {
                col.GetComponent<EnemyHealth>().TakeDamage(FindObjectOfType<MintAttack>().damage);
            } else if (col.GetComponent<FallHealth>() != null) {
                col.GetComponent<FallHealth>().TakeDamage(FindObjectOfType<MintAttack>().damage);
            }
            foreach (GameObject hitEffect in hitEffects) {
                GameObject hitFX = Instantiate(hitEffect, transform.localPosition, Quaternion.identity);
                Destroy(hitFX, 5f);
                Destroy(gameObject);
            }
        }
    }
}
