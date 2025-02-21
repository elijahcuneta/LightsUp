using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerUpsSpawner : MonoBehaviour {

    [SerializeField]
    EnemyPowerUps orb;

    [SerializeField]
    float radiusOfExplosion = 10;

    [SerializeField]
    Transform spawnPowerUps;

    [SerializeField]
    int orbCount;

    EnemyHealth enemyHealth;
    bool releasedOrb;

    void Start() {
        enemyHealth = GetComponentInChildren<EnemyHealth>();    
    }

    void Update () {
        if (enemyHealth.isDead && !releasedOrb) {
            for(int i = 0; i < orbCount; i++) {
                EnemyPowerUps newOrb = Instantiate(orb, spawnPowerUps.position, transform.rotation) as EnemyPowerUps;
                newOrb.orbBehavior += Died;
                newOrb.GetComponent<Rigidbody>().velocity = Random.onUnitSphere * -radiusOfExplosion;
            }
            releasedOrb = true;
        }
	}

    public void Died(EnemyPowerUps currentOrb) {
        currentOrb.orbBehavior -= Died;
    }


}
