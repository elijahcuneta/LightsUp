using System.Collections;
using UnityEngine;

public class PowerUps : MonoBehaviour {

    [SerializeField]
    Vector3 offset;

    [SerializeField]
    float speed = 1, manaValue = 0.5f;


    GameObject player;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            StartCoroutine(moveToPlayer());
        }
    }

    IEnumerator moveToPlayer() {
        while (transform.position != player.transform.position - offset) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position - offset, speed * Time.deltaTime);
            yield return null;
        }
        StopAllCoroutines();
        FindObjectOfType<PlayerOrbBehavior>().TwinkleOrb();
        PlayerHealth.currentMana += manaValue;
        Destroy(gameObject);
    }
}
