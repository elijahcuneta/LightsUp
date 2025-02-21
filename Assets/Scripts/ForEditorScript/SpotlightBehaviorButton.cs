#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpotlightBehavior))]
public class SpotlightBehaviorButton : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        SpotlightBehavior sBehavior = (SpotlightBehavior)target;

        if (GUILayout.Button("Set Start Position")) {
            Undo.RecordObject(target, "Set Start Position");
            sBehavior.clickedSetStartPosition();
        }
        if (GUILayout.Button("Set End Position")) {
            Undo.RecordObject(target, "Set End Position");
            sBehavior.clickedSetEndPosition();
        }
    }
}
#endif