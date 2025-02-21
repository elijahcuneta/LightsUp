using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerUps : MonoBehaviour {

    public delegate void OrbBehavior(EnemyPowerUps orbBehavior);
    public event OrbBehavior orbBehavior;

    [SerializeField]
    int minimumLifeTime = 2, maximumLifeTime = 3;

    void Start () {
        Invoke("DestroyOrb", Random.Range(minimumLifeTime, maximumLifeTime));
	}
	
    void DestroyOrb() {
        if(orbBehavior != null) {
            orbBehavior(this);
        }

        Destroy(gameObject);
    }
    
}
