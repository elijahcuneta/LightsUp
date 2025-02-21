using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallNormalSphere : MonoBehaviour {

    [SerializeField]
    float startRadius, endRadius, deploySpeed = 5f;
    
    [SerializeField]
    float addingValueToRadius = 0.1f;

    [SerializeField]
    int normalSphereDamage = 5;

    Transform attackPosition;

    [HideInInspector]
    public bool deploy;

    void Start () {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;

        attackPosition = FindObjectOfType<FallAttack>().attackPosition;
        StartCoroutine(GrowRadius(attackPosition));
    }

    IEnumerator GrowRadius(Transform attackPosition) {
        Vector3 targetRadius = Vector3.one * endRadius;
        transform.localScale = Vector3.one * startRadius;

        if (GetComponent<SphereCollider>() != null) {
            GetComponent<SphereCollider>().enabled = false;
        }

        while (!deploy) {
            transform.localScale += Vector3.one * addingValueToRadius * Time.deltaTime;
            transform.localPosition = new Vector3(attackPosition.position.x, attackPosition.position.y, attackPosition.position.z) + new Vector3(0, 0.02f, 0);
            if(transform.localScale.x >= targetRadius.x) {
                transform.localScale = targetRadius;
            }
            yield return new WaitForEndOfFrame();
        }

        transform.localScale = targetRadius;
        StopAllCoroutines();
    }

    public void Deploy(Vector3 target) {
        StopAllCoroutines();
        if(GetComponent<SphereCollider>() != null) {
            GetComponent<SphereCollider>().enabled = true;
        }
        StartCoroutine(AttackTarget(target));    
    }

    IEnumerator AttackTarget(Vector3 target) {
        if(target != null) {
            while (transform.position != target) {
                transform.position = Vector3.MoveTowards(transform.position, target, deploySpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
        Destroy(gameObject);
        StopAllCoroutines();
    }

    void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            col.GetComponent<PlayerHealth>().TakeDamage(normalSphereDamage);
            Destroy(gameObject);
        }
    }
}