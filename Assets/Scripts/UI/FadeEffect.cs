using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour {

    public Texture2D fadeOutTexture;                  
    public float fadeSpeed = 0.8f;     

    private int drawDepth = -1000;                 
    [HideInInspector]
    public float alpha = 1.0f;           
    private int fadeDir = -1;              

    void OnGUI() {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp(alpha, 0f, 1f);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;                         
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);            
    }

    public float BeginFade(int direction) {
        fadeDir = direction;
        return 1 / fadeSpeed;
    }

    void OnLevelWasLoaded() {
        BeginFade(-1);
    }

    public float getAlpha() {
        return alpha;
    }

    public void setAlpha(float alphaValue) {
        alpha = alphaValue;
    }
}
