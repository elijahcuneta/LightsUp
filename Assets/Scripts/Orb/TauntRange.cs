using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntRange : MonoBehaviour {

    [HideInInspector]
    public bool isActive;

    void OnTriggerStay(Collider col) {
        if(col.tag == "Enemy" && col.GetComponent<EnemyMovement>() != null && isActive) {
            if(col.GetComponent<Caterpy>() != null) {
                col.GetComponent<Caterpy>().StopAllCoroutines();
            } else if(col.GetComponent<Cruncher>() != null) {
                col.GetComponent<Cruncher>().StopAllCoroutines();
            }

            col.GetComponent<EnemyMovement>().Turning(transform, 150);
            col.GetComponent<EnemyMovement>().Movement(0.1f);
        }
    }
}
