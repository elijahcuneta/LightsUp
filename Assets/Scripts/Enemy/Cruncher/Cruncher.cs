using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cruncher : MonoBehaviour {

    [SerializeField]
    float chargeDuration, rotationSpeed, distanceFromStanceToCharge, dashPower = 10, dragWhenAttacking = 4f;

    [SerializeField]
    int damageHitsMe;

    EnemyMovement cruncherMov;
    EnemyAttack cruncherAtk;
    EnemyHealth cruncherHP;
    Rigidbody cruncherRB;
    Animator cruncherAnim;

    bool hit, charging, attacking, dead;
    float currentHealth;

    void Start () {
        cruncherMov = GetComponent<EnemyMovement>();
        cruncherAtk = GetComponent<EnemyAttack>();
        cruncherHP = GetComponent<EnemyHealth>();
        cruncherRB = GetComponent<Rigidbody>();
        cruncherAnim = GetComponentInChildren<Animator>();
	}

    void Update() {
        Animating();
        if(currentHealth != cruncherHP.getCurrentHealth() && cruncherHP.getCurrentHealth() != 0) {
            hit = true;
        }
        currentHealth = cruncherHP.getCurrentHealth();
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            StartCoroutine(CruncherCharge(col.transform));
        }
    }

    void OnTriggerStay(Collider col) {
        if (col.tag == "Player") {
            FindObjectOfType<PlayerOrbColorIndicator>().lightMode_Cruncher();
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Player") {
            FindObjectOfType<PlayerOrbColorIndicator>().lightMode_Original();
        }
    }

    IEnumerator CruncherCharge(Transform target) {
        float distanceBack = transform.position.z + distanceFromStanceToCharge;
        float elapsedTime = 0;
        Vector3 startingPosition = transform.position;
        Vector3 chargePosition = new Vector3(transform.position.x, transform.position.y, distanceBack);
        cruncherRB.isKinematic = false;

        while (elapsedTime < 1) {
            charging = true;
            elapsedTime += Time.deltaTime / chargeDuration;
            cruncherMov.Turning(target, rotationSpeed);
            transform.position -= transform.forward * elapsedTime * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        charging = false;
        StopAllCoroutines();
        StartCoroutine(CruncherAttack());
    }

    IEnumerator CruncherAttack() {
        attacking = true;
        float elapsedTime = 0;
        cruncherRB.AddForce(transform.forward * dashPower * 10);
        while (elapsedTime < 1) {
            elapsedTime += Time.deltaTime / chargeDuration;
            cruncherRB.drag = dragWhenAttacking;
            yield return new WaitForEndOfFrame();
        }
        StopAllCoroutines();
        cruncherRB.isKinematic = true;
        cruncherRB.drag = 0f;
        attacking = false;
    }

    void Animating() {
       if (hit) {
            cruncherAnim.SetTrigger("Hit");
            hit = false;
        } else if (cruncherHP.isDead && !dead) {
            cruncherAnim.SetTrigger("Death");
            dead = true;
        }
        cruncherAnim.SetBool("Charge Attack", charging);
        cruncherAnim.SetBool("Attack", attacking);
    }

    public void shutdownMovement() {
        StopAllCoroutines();
    }
}
