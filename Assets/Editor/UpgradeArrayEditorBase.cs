using UnityEditor;
using UnityEngine;

public class UpgradeArrayEditorBase : Editor
{
    protected SerializedProperty upgradesProperty;
    protected SerializedProperty maxLevelProperty;
    protected SerializedProperty basePriceProperty;

    private void OnEnable()
    {
        if (target is IUpgrade)
        {
            upgradesProperty = serializedObject.FindProperty("priceUpgradeForLevels");
            maxLevelProperty = serializedObject.FindProperty("maxLevels");
            basePriceProperty = serializedObject.FindProperty("basePrice");
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (maxLevelProperty.intValue < 0) maxLevelProperty.intValue = 0;

        DrawPropertiesExcluding(serializedObject, new string[] { "basePrice", "maxLevels", "priceUpgradeForLevels" });

        EditorGUILayout.Space(); 
        EditorGUILayout.LabelField("Parameters", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(maxLevelProperty);
        EditorGUILayout.PropertyField(basePriceProperty);

        if (target is IUpgrade)
        {
            int maxLevel = maxLevelProperty.intValue + 1;
            if (upgradesProperty.arraySize != maxLevel)
            {
                upgradesProperty.arraySize = maxLevel;
            }

            if (upgradesProperty.isArray && upgradesProperty.arraySize > 0)
            {
                for (int i = 1; i < upgradesProperty.arraySize; i++)
                {
                    SerializedProperty element = upgradesProperty.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(element, new GUIContent($"Upgrade {i}"));
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            base.OnInspectorGUI();
        }
    }
}

[CustomEditor(typeof(SpeedMovementUpgrade))]
public class SpeedMovementUpgradeEditor : UpgradeArrayEditorBase
{
    // Ќет необходимости в дополнительном коде, т.к. все уже определено в базовом классе
}
[CustomEditor(typeof(CapacityUpgrade))]
public class CapacityUpgradeEditor : UpgradeArrayEditorBase
{
    // Ќет необходимости в дополнительном коде, т.к. все уже определено в базовом классе
}
[CustomEditor(typeof(SpeedActionUpgrade))]
public class SpeedActionUpgradeEditor : UpgradeArrayEditorBase
{
    // Ќет необходимости в дополнительном коде, т.к. все уже определено в базовом классе
}

