using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallHealth : MonoBehaviour {

    [Range(0,100)]
    [SerializeField]
    float hpTrig_BlindAtk = 50, hpTrig_RayAtk = 25, hpTrig_NearDeath = 15;

    const float fullHealth = 100;
    float health = fullHealth;
    public float Health { get { return health;  } set { health = value; } }

    bool isDead;
    public bool IsDead { get { return isDead; } }

    [HideInInspector]
    public bool blindAtkAdd, rayAtkAdd, nearDeath;

    public delegate void FallDead();
    public event FallDead fallDead;

    public void TakeDamage(float damage) {
        health -= damage;

        if(health <= hpTrig_BlindAtk && !blindAtkAdd) {
            blindAtkAdd = true;
        } else if(health <= hpTrig_RayAtk && !rayAtkAdd) {
            rayAtkAdd = true;
        } else if (health <= hpTrig_NearDeath && !nearDeath) {
            nearDeath = true;
        } else if(health <= 0 && !isDead) {
            Death();
        }
    }

    public void Death() {
        isDead = true;
        if(GetComponent<BoxCollider>() != null) {
            GetComponent<BoxCollider>().enabled = false;
        }
        fallDead();
    }
}
