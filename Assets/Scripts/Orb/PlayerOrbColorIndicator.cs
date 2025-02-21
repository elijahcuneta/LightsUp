using System.Collections;
using UnityEngine;

public class PlayerOrbColorIndicator : MonoBehaviour {

    Light[] peterOrbLightMaterial;
    Material peterOrbMaterial;
    Color destinationColor, originalColor;


    [SerializeField]
    float smooth = 2;

    public Color interactableColor, caterpyWarningColor, cruncherWarningColor, jaboxyWarningColor;

    void Awake() {
        peterOrbLightMaterial = GetComponentsInChildren<Light>();
        peterOrbMaterial = GetComponentInChildren<MeshRenderer>().material;

        originalColor = peterOrbMaterial.color;
        destinationColor = originalColor;

    }

    IEnumerator switchLightColor() {
        while (peterOrbLightMaterial[0].color != destinationColor) {
            foreach (Light light in peterOrbLightMaterial) {
                light.color = Color.Lerp(light.color, destinationColor, Time.deltaTime * smooth);
                peterOrbMaterial.SetColor("_EmissionColor", Color.Lerp(light.color, destinationColor, Time.deltaTime * smooth));
                if (light.color == destinationColor) {
                    StopAllCoroutines();
                }
            }
            yield return null;
        }
    }

    void StartLerping() {
        StopAllCoroutines();
        StartCoroutine(switchLightColor());
    }

    public void lightMode_Interactable() {
        destinationColor = interactableColor;
        StartLerping();
    }

    public void lightMode_Original() {
        destinationColor = originalColor;
        StartLerping();
    }

    public void lightMode_Caterpy() {
        destinationColor = caterpyWarningColor;
        StartLerping();
    }

    public void lightMode_Cruncher() {
        destinationColor = cruncherWarningColor;
        StartLerping();
    }

    public void lightMode_Jaboxy() {
        destinationColor = jaboxyWarningColor;
        StartLerping();
    }

}
