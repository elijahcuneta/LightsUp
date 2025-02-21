using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float startingHealth;

    [HideInInspector]
    public bool isDead, isHit;

    private float currentHealth;

    void Awake() {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damageTaken) {
        currentHealth -= damageTaken;


        if (currentHealth <= 0 && !isDead) {
            Death();
        }
    }

    public void Death() {
        isDead = true;
        if(GetComponent<Rigidbody>() != null) {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        if (GetComponent<SphereCollider>() != null) {
            GetComponent<SphereCollider>().enabled = false;
        }
        if (GetComponent<BoxCollider>() != null) {
            GetComponent<BoxCollider>().enabled = false;
        }
        if(GetComponent<EnemyMovement>() != null) {
            GetComponent<EnemyMovement>().freezeEnemy();
        }

        if (GetComponent<Caterpy>() != null) {
            GetComponent<Caterpy>().shutDownMovement();
        } else if(GetComponent<Cruncher>() != null) {
            GetComponent<Cruncher>().shutdownMovement();
        }
    }

    public float getCurrentHealth() {
        return currentHealth;
    }

}
