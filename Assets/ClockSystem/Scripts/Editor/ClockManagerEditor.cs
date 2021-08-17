using UnityEditor;
using Clocks;

[CustomEditor(typeof(ClockManager))]
public class ClockManagerEditor : Editor
{
    SerializedProperty runtimeSet;
    SerializedProperty timeReference;
    SerializedProperty timeSpecifiedStart;

    void OnEnable()
    {
        runtimeSet = serializedObject.FindProperty("_runtimeSet");
        timeReference = serializedObject.FindProperty("_timeReference");
        timeSpecifiedStart = serializedObject.FindProperty("_timeSpecifiedStart");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(runtimeSet);

        EditorGUILayout.PropertyField(timeReference);

        if (timeReference.enumValueIndex == (int)ClockManager.TimeRefernces.SpecifiedTime)
        {
            EditorGUILayout.PropertyField(timeSpecifiedStart);
        }
        else if (timeReference.enumValueIndex == (int)ClockManager.TimeRefernces.TimeProvider)
        {
            EditorGUILayout.LabelField("(Attach a script to this GameObject that inherits from ITimeProvider.)");
        }

        serializedObject.ApplyModifiedProperties();
    }
}
