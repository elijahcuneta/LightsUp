using System.Collections;
using UnityEngine;

public class Caterpy : MonoBehaviour {

    EnemyMovement caterpyMov;
    EnemyAttack caterpyAtk;
    EnemyHealth caterpyHealth;

    Rigidbody rbCaterpy;

    Animator caterpyAnim;

    [SerializeField]
    float timeWaitToStop = 3, timeWaitToMove = 2, timeWaitToCharge = 1, rotationSpeed, distanceRayToAttack = 1;

    [SerializeField]
    int damageHitsMe = 1;

    [SerializeField]
    string[] attackMode;

    float timer;

    int randomIndex;

    void Awake() {
        caterpyMov = GetComponent<EnemyMovement>();
        caterpyHealth = GetComponent<EnemyHealth>();
        caterpyAtk = GetComponent<EnemyAttack>();

        caterpyAnim = GetComponentInChildren<Animator>();
        rbCaterpy = GetComponent<Rigidbody>();

        damageHitsMe = caterpyAtk.damage;
    }

    void Update() {
        Animating();
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            startOverVariables();
            StopCoroutine(CaterpyMove(col));
            StartCoroutine(CaterpyCharge(col));
        }
        
    }

    void OnTriggerStay(Collider col) {
        if(col.tag == "Player") {
            FindObjectOfType<PlayerOrbColorIndicator>().lightMode_Caterpy();
        }
    }

    IEnumerator CaterpyMove(Collider col) {
        while (timer <= timeWaitToStop) {
            timer += Time.deltaTime;
            caterpyMov.Turning(col.transform, rotationSpeed);
            caterpyMov.Movement(Vector3.MoveTowards(transform.position, col.transform.position, caterpyMov.speed * Time.deltaTime).z);
            Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward);
            RaycastHit target;
            if (Physics.Raycast(ray, out target, distanceRayToAttack)) {
                if (target.collider.tag == "Player") {
                    stopMove();
                    attack();
                }
            }
            yield return new WaitForEndOfFrame();
        }
        rbCaterpy.isKinematic = true;
        StopCoroutine(CaterpyMove(col));
        StartCoroutine(CaterpyCooldownBeforeCharge(col));
    }

    IEnumerator CaterpyCharge(Collider col) {
        yield return new WaitForSeconds(timeWaitToMove);
        rbCaterpy.isKinematic = false;
        timer = 0;
        StopCoroutine(CaterpyCharge(col));
        StartCoroutine(CaterpyMove(col));
    }

    IEnumerator CaterpyCooldownBeforeCharge(Collider col) {
        yield return new WaitForSeconds(timeWaitToCharge);
        StopCoroutine(CaterpyCooldownBeforeCharge(col));
        StartCoroutine(CaterpyCharge(col));
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Player") {
            FindObjectOfType<PlayerOrbColorIndicator>().lightMode_Original();
            rbCaterpy.isKinematic = true;
            startOverVariables();
        }
    }

    void startOverVariables() {
        timer = 0;
        StopAllCoroutines();
    }

    void attack() {
        randomIndex = Random.Range(0, attackMode.Length);
        caterpyAnim.SetTrigger(attackMode[randomIndex]);
        GetComponent<EnemyMovement>().resetVelocity();
    }

    void stopMove() {
        timer = timeWaitToStop + 1;
    }

    void Animating() {
        caterpyAnim.SetBool("Walk", caterpyMov.move);
        if (caterpyHealth.isDead && !caterpyAnim.GetCurrentAnimatorStateInfo(0).IsName("Death")) {
            caterpyAnim.SetTrigger("Death");
        }
    }

    public void shutDownMovement() {
        StopAllCoroutines();
    }

}
