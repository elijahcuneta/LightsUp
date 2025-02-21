using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    enum PlayerStatus { Normal, Hit, LowHP }

    [HideInInspector]
    public PlayerSkills playerSkills = PlayerSkills.Taunt;

    [SerializeField]
    Image HPImage, HPRedImage, ManaImage, playerImage;

    [SerializeField]
    Sprite[] peterImage, mintImage;

    [SerializeField]
    RectTransform skillsSet;

    [SerializeField]
    float switchSkillSpeed = 200f;

    bool peterPicked;

    float origPlayerHP, origPlayerMana;

	void Start () {
        DontDestroyOnLoad(gameObject);
        origPlayerHP = PlayerHealth.originalHealth;
        origPlayerMana = PlayerHealth.originalMana;

        if(GameObject.Find("Peter") != null) {
            playerImage.sprite = peterImage[(int)PlayerStatus.Normal];
            peterPicked = true;
        } else if(GameObject.Find("Mint") != null) {
            playerImage.sprite = mintImage[(int)PlayerStatus.Normal];
            peterPicked = false;
        }

        playerSkills = PlayerSkills.Taunt;
    }
	
	void Update () {
        HPImage.fillAmount = PlayerHealth.currentHealth / origPlayerHP;
        ManaImage.fillAmount = PlayerHealth.currentMana / origPlayerMana;

        if(HPImage.fillAmount != 1) {
            StartCoroutine(delayHPBar(PlayerHealth.currentHealth / origPlayerHP));
        }

        if(HPImage.fillAmount > HPRedImage.fillAmount) {
            HPRedImage.fillAmount = HPImage.fillAmount;
        }
        

        if (HPImage.fillAmount > 0.5f && HPImage.color != new Color32(0, 255, 55, 255)) {
            HPImage.color = new Color32(0, 255, 55, 255);
            playerImage.sprite = peterPicked ? peterImage[(int)PlayerStatus.Normal] : mintImage[(int)PlayerStatus.Normal];
        } else if (HPImage.fillAmount < 0.5f && HPImage.fillAmount > 0.3f && HPImage.color != new Color32(255, 195, 0, 255)) {
            HPImage.color = new Color32(255, 195, 0, 255);
            playerImage.sprite = peterPicked ? peterImage[(int)PlayerStatus.Normal] : mintImage[(int)PlayerStatus.Normal];
        } else if (HPImage.fillAmount < 0.3f && HPImage.color != new Color32(255, 0, 0, 255)) {
            HPImage.color = new Color32(255, 0, 0, 255);
            playerImage.sprite = peterPicked ? peterImage[(int)PlayerStatus.LowHP] : mintImage[(int)PlayerStatus.LowHP];
        }

        if (InputManager.RightBumper_ButtonDown()) {
           if(skillsSet.localPosition == Vector3.zero) {
                playerSkills = PlayerSkills.Escape;
                StopAllCoroutines();
               StartCoroutine(switchSkill(new Vector3(-43, 0, 0)));
           } else {
                playerSkills = PlayerSkills.Taunt;
                StopAllCoroutines();
               StartCoroutine(switchSkill(Vector3.zero));
            }
        }
    }

    IEnumerator switchSkill (Vector3 desiredPosition) {
        while(skillsSet.localPosition != desiredPosition) {
            skillsSet.localPosition = Vector3.MoveTowards(skillsSet.localPosition, desiredPosition, switchSkillSpeed * Time.unscaledDeltaTime);
            yield return new WaitForEndOfFrame();
        }
        skillsSet.localPosition = desiredPosition;
    }

    IEnumerator delayHPBar (float desiredFillAmount) {
        yield return new WaitForSecondsRealtime(2f);
        while(HPRedImage.fillAmount >= desiredFillAmount) {
            HPRedImage.fillAmount = Mathf.Lerp(HPRedImage.fillAmount, desiredFillAmount, switchSkillSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
