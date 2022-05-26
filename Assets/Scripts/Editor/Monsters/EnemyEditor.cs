using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy), true)]
public class EnemyEditor : Editor
{
    SerializedProperty healthProp;
    SerializedProperty e_health;
    SerializedProperty splatterProp;
    SerializedProperty hitImpactProp;
    SerializedProperty startStunTimeProp;
    SerializedProperty stunEffectProp;
    float labelWidth = 150f;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        healthProp = serializedObject.FindProperty("maxHealth");
        e_health = serializedObject.FindProperty("health");
        splatterProp = serializedObject.FindProperty("deathSplatter");
        hitImpactProp = serializedObject.FindProperty("hitImpact");
        startStunTimeProp = serializedObject.FindProperty("startStunTime");
        stunEffectProp = serializedObject.FindProperty("stunEffect");
    }
    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();
        GUILayout.Label("Max Health of the enemy", GUILayout.Width(labelWidth)); //5

        EditorGUILayout.PropertyField(e_health);
        EditorGUILayout.Space();
        // Show the custom GUI controls.
        EditorGUILayout.Slider(healthProp, 0, 100, new GUIContent("Health"));

        // Only show the damage progress bar if all the objects have the same damage value:
        if (!healthProp.hasMultipleDifferentValues)
            ProgressBar(healthProp.floatValue / 100.0f, "Health");

        EditorGUILayout.Space();
      

        EditorGUILayout.PropertyField(splatterProp, new GUIContent("Death effect") , true);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(hitImpactProp, new GUIContent("Hit impact"), true);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(startStunTimeProp);
        EditorGUILayout.PropertyField(stunEffectProp, new GUIContent("Effect Stunned"));
        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }

    // Custom GUILayout progress bar.
    void ProgressBar(float value, string label)
    {
        // Get a rect for the progress bar using the same margins as a textfield:
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }


}
