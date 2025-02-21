using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public int damage;

    AnimatorStateInfo enemStateInfo;
    Animator enemAnim;

    bool hit;

    float timer;

    void Awake() {
        enemAnim = GetComponentInChildren<Animator>();
    }
    void Update() {
        enemStateInfo = enemAnim.GetCurrentAnimatorStateInfo(0);

        if (hit) {
            timer += Time.deltaTime;
            if (timer >= 3) {
                hit = false;
                timer = 0;
            }
        }
    }
    void OnCollisionStay(Collision col) {
        if (col.collider.tag == "Player" && enemStateInfo.IsTag("Attack") && !hit) {
            col.collider.GetComponent<Rigidbody>().AddForce(transform.forward * 150f);
            col.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
            hit = true;
        }
    }

}
