using System.Collections;
using UnityEngine;

public class PlayerOrbBehavior : MonoBehaviour {

    public Vector3 offset;

    float lightIntenAnimSpeed = 0.1f, origLightIntenValue;

    [Range(0f, 1f)]
    public float orbNavigatingSpeed = 0.5f;

    [SerializeField]
    float orbDistanceToPlayerTreshold = 5f;


    Light[] peterOrbLightMaterial;
    Transform origTarget, target;
    Vector3 currentTarget;

    PlayerSkill playerSkill;

    TauntRange tauntRange;

    bool orb_Travel, goBack = true;

    void Start() {
        peterOrbLightMaterial = GetComponentsInChildren<Light>();
        origLightIntenValue = peterOrbLightMaterial[0].intensity;

        target = GameObject.FindGameObjectWithTag("Player").transform;
        origTarget = target;
        transform.parent = origTarget;
        currentTarget = origTarget.position;

        tauntRange = GetComponentInChildren<TauntRange>();
        playerSkill = GetComponentInParent<PlayerSkill>();
    }

    void Update() {
        if (orb_Travel) {
            transform.position = currentTarget - offset;
        }
        if(transform.parent == null && Vector3.Distance(transform.localPosition, origTarget.localPosition) > orbDistanceToPlayerTreshold) {
            BackToOriginalPosition();
        }
    }

    public void NavigateTarget(Vector3 myTarget) {
        StopAllCoroutines();
        StartCoroutine(navigateOrbToTarget(myTarget, false));
    }

    public void BackToOriginalPosition() {
        StopAllCoroutines();
        StartCoroutine(navigateOrbToTarget(origTarget.localPosition, true));
    }

    public void TwinkleOrb() {
        StopAllCoroutines();
        StartCoroutine(changeOrbLightsIntensity(peterOrbLightMaterial[0].intensity + 0.2f));
    }

    public IEnumerator Taunting(float tauntDuration) {
        tauntRange.isActive = true;
        transform.parent = null;
        yield return new WaitForSeconds(tauntDuration);
        StopCoroutine(Taunting(tauntDuration));
        BackToOriginalPosition();
    }

    public IEnumerator Escaping(float escapeDuration) {
        yield return new WaitForSeconds(escapeDuration);
        StopCoroutine(Escaping(escapeDuration));
        playerSkill.ReturnPlayerStat();
    }

    IEnumerator navigateOrbToTarget(Vector3 positionTarget, bool owner) {
        orb_Travel = true;
        transform.parent = null;
        while (currentTarget != positionTarget) {
            currentTarget = Vector3.MoveTowards(currentTarget, positionTarget, orbNavigatingSpeed);
            yield return null;
        }
        if (owner) {
            transform.parent = origTarget;
            transform.localPosition = new Vector3(0, 1.2f, 0);
            playerSkill.usingSkill = false;
            tauntRange.isActive = false;
        }
        orb_Travel = false;
        StopAllCoroutines();
    }

    IEnumerator changeOrbLightsIntensity(float intensityValue) {
        while (peterOrbLightMaterial[0].intensity <= intensityValue) {
            foreach (Light l in peterOrbLightMaterial) {
                l.intensity += lightIntenAnimSpeed;
            }
            yield return null;
        }
        peterOrbLightMaterial[0].intensity = intensityValue;
        StopAllCoroutines();
        StartCoroutine(returnOrigOrbLightIntensity());
    }

    IEnumerator returnOrigOrbLightIntensity() {
        while (peterOrbLightMaterial[0].intensity >= origLightIntenValue) {
            foreach (Light l in peterOrbLightMaterial) {
                l.intensity -= lightIntenAnimSpeed;
            }
            yield return null;
        }
        peterOrbLightMaterial[0].intensity = origLightIntenValue;
        StopAllCoroutines();
    }

}
