using UnityEngine;

public class Sword : MonoBehaviour {

    [SerializeField]
    Transform hitSpawn;

    [SerializeField]
    GameObject[] hitEffects;

    void OnTriggerEnter(Collider col) {
        if (FindObjectOfType<PeterAttack>().attacking && col.tag == "Enemy" && (col.GetComponent<EnemyHealth>() != null || col.GetComponent<FallHealth>() != null)) {
            if(col.GetComponent<EnemyHealth>() != null) {
                col.GetComponent<EnemyHealth>().TakeDamage(transform.root.GetComponent<PeterAttack>().damage);
            } else if (col.GetComponent<FallHealth>() != null){
                col.GetComponent<FallHealth>().TakeDamage(transform.root.GetComponent<PeterAttack>().damage);
            }
            foreach (GameObject hitEffect in hitEffects) {
                GameObject hitFX = Instantiate(hitEffect, hitSpawn.position, Quaternion.identity);
                Destroy(hitFX, 5f);
            }
        }
    }

}
