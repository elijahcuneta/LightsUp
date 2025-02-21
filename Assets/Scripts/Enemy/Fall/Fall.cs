using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {

    [SerializeField]
    float delayBetweenAttacks;

    FallHealth fallHealth;
    FallAttack fallAttack;

    Animator fallAnim;
    AnimatorStateInfo fallAnimStateInfo;

    bool blindAtkAdded, rayAtkAdded, nearDeath;
    bool castAttack;

    void Awake () {
        fallHealth = GetComponent <FallHealth>();
        fallAttack = GetComponent<FallAttack>();
        fallAnim = GetComponent<Animator>();

        fallHealth.fallDead += FallDead;

        StartCoroutine(StartAttack());
    }

    void Update() {
        fallAnimStateInfo = fallAnim.GetCurrentAnimatorStateInfo(0);

        if (fallHealth.blindAtkAdd && !blindAtkAdded) {
            fallAttack.AddAttack(fallAttack.BlindAttack);
            fallAnim.SetTrigger("Hit 1");
            blindAtkAdded = true;
        } else if (fallHealth.rayAtkAdd && !rayAtkAdded) {
            fallAttack.AddAttack(fallAttack.RayAttack);
            fallAnim.SetTrigger("Hit 2");
            rayAtkAdded = true;
        } else if (fallHealth.nearDeath && !nearDeath) {
            delayBetweenAttacks /= 2;
            delayBetweenAttacks = Mathf.Round(delayBetweenAttacks);
            fallAnim.SetTrigger("Hit 3");
            nearDeath = true;
        }

        if(castAttack && fallAttack.attackMode == FallAttack.AttackMode.Normal) {
            fallAnim.SetTrigger("Normal Attack " + Random.Range(1, 3));
            castAttack = false;
        } else if (castAttack && fallAttack.attackMode == FallAttack.AttackMode.Blind) {
            fallAnim.SetTrigger("Blinding Attack");
            castAttack = false;
        } else if (castAttack && fallAttack.attackMode == FallAttack.AttackMode.Ray) {
            fallAnim.SetTrigger("Ray of Light");
            castAttack = false;
        }
    }

    IEnumerator StartAttack() {
        if (!fallAnimStateInfo.IsTag("Hit")) {
            castAttack = true;
            fallAttack.StartAttack();
        }
        yield return new WaitForSeconds(fallAnimStateInfo.length + fallAnimStateInfo.normalizedTime);
        yield return new WaitForSeconds(delayBetweenAttacks);
        StopCoroutine(StartAttack());
        if (!fallHealth.IsDead) {
            StartCoroutine(StartAttack());
        }
    }

    public void FallDead() {
        Debug.Log("FallClass: Dead");
        fallAnim.SetTrigger("Death");

        fallHealth.fallDead -= FallDead;
    }
}
