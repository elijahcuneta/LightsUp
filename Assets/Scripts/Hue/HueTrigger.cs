using UnityEngine;

public class HueTrigger : MonoBehaviour {

    PlayerOrbChangeHue playerOrb;

    enum HueMode { Normal, Hidden, Move, ReverseHidden };
    Collider gameObjectCollider;
    string previousHueMode, currentHueMode;
    
    bool reverseHiddenType;

    void Start() {
        playerOrb = FindObjectOfType<PlayerOrbChangeHue>();
        gameObjectCollider = GetComponent<Collider>();
    }

    void Update() {
        if(playerOrb != null) {
            currentHueMode = playerOrb.getHueMode();
        }
        if (previousHueMode != currentHueMode) {
            if (playerOrb.getHueMode() == HueMode.Normal.ToString()) {
                HueMode_Normal();
            } else if (playerOrb.getHueMode() == HueMode.Hidden.ToString()) {
                HueMode_Hidden();
                if (LayerMask.LayerToName(gameObject.layer) == HueMode.ReverseHidden.ToString()) {
                    HueMode_ReverseHidden();
                }
            } else if (playerOrb.getHueMode() == HueMode.Move.ToString()) {
                HueMode_Move();
            }
            previousHueMode = playerOrb.getHueMode();
        }
    }

    void HueMode_Normal() {
        if(gameObjectCollider != null)
            gameObjectCollider.enabled = true;
        if (LayerMask.LayerToName(gameObject.layer) == HueMode.Hidden.ToString()) {
            if (gameObjectCollider != null)
                gameObjectCollider.enabled = false;
        }

        SetActive_Animation(false);
    }

    void HueMode_Hidden() {
        if (gameObjectCollider != null)
            gameObjectCollider.enabled = true;
         if (LayerMask.LayerToName(gameObject.layer) == HueMode.Normal.ToString() || LayerMask.LayerToName(gameObject.layer) == HueMode.Move.ToString()) {
            if (gameObjectCollider != null)
                gameObjectCollider.enabled = false;
         }

        SetActive_Animation(false);
    }

    void HueMode_ReverseHidden() {
       if (gameObjectCollider != null)
           gameObjectCollider.enabled = false;
        SetActive_Animation(false);
    }

    void HueMode_Move() {
        HueMode_Normal();
        if (GetComponent<Animator>() != null) {
            SetActive_Animation(true);
        }
    }

    void SetActive_Animation(bool isActive) {
        if (GetComponent<Animator>() != null) {
            GetComponent<Animator>().SetBool("Active", isActive);
        }
    }



}
