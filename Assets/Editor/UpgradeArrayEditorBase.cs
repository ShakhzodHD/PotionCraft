using UnityEditor;
using UnityEngine;

public class UpgradeArrayEditorBase : Editor
{
    protected SerializedProperty upgradesProperty;
    protected SerializedProperty basePriceProperty;
    protected SerializedProperty maxLevelProperty;
    protected SerializedProperty numberSpeedProperty;
    protected SerializedProperty inventoryLimitProperty;
    protected SerializedProperty delayUpgradeProperty;

    protected virtual void OnEnable()
    {
        if (target is IUpgrade)
        {
            upgradesProperty = serializedObject.FindProperty("priceUpgradeForLevels");
            maxLevelProperty = serializedObject.FindProperty("maxLevels");
            basePriceProperty = serializedObject.FindProperty("basePrice");
            numberSpeedProperty = serializedObject.FindProperty("numberUpgradeForMovementSpeed");
            inventoryLimitProperty = serializedObject.FindProperty("inventoryLimitUpgradeForLevels");
            delayUpgradeProperty = serializedObject.FindProperty("delayUpgradeForLevels");
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (maxLevelProperty.intValue < 0) maxLevelProperty.intValue = 0;

        DrawPropertiesExcluding(serializedObject, new string[] { "basePrice", "baseSpeed", "maxLevels",
            "priceUpgradeForLevels", "numberUpgradeForMovementSpeed", "baseInventoryLimit", "delayUpgradeForLevels",
            "inventoryLimitUpgradeForLevels", "baseDelay" });

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Parameters", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(maxLevelProperty);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(basePriceProperty);
        DrawSpecificProperties();

        if (target is IUpgrade)
        {
            int maxLevel = maxLevelProperty.intValue + 1;
            AdjustArraySizes(maxLevel);

            if (upgradesProperty.isArray && upgradesProperty.arraySize > 0)
            {
                DrawUpgradeProperties(maxLevel);
            }

            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            base.OnInspectorGUI();
        }
    }

    protected virtual void DrawSpecificProperties() { }

    private void AdjustArraySizes(int size)
    {
        if (upgradesProperty.arraySize != size)
        {
            upgradesProperty.arraySize = size;
        }

        if (numberSpeedProperty != null && numberSpeedProperty.arraySize != size)
        {
            numberSpeedProperty.arraySize = size;
        }

        if (inventoryLimitProperty != null && inventoryLimitProperty.arraySize != size)
        {
            inventoryLimitProperty.arraySize = size;
        }

        if (delayUpgradeProperty != null && delayUpgradeProperty.arraySize != size)
        {
            delayUpgradeProperty.arraySize = size;
        }
    }

    private void DrawUpgradeProperties(int maxLevel)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Upgrade Price", GUILayout.Width(150));
        if (numberSpeedProperty != null)
            EditorGUILayout.LabelField("Speed Increase", GUILayout.Width(150));
        if (inventoryLimitProperty != null)
            EditorGUILayout.LabelField("Inventory Limit", GUILayout.Width(150));
        if (delayUpgradeProperty != null)
            EditorGUILayout.LabelField("Delay", GUILayout.Width(150));
        EditorGUILayout.EndHorizontal();

        for (int i = 1; i < maxLevel; i++)
        {
            EditorGUILayout.BeginHorizontal();
            SerializedProperty priceElement = upgradesProperty.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(priceElement, GUIContent.none, GUILayout.Width(150));

            if (numberSpeedProperty != null)
            {
                SerializedProperty speedElement = numberSpeedProperty.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(speedElement, GUIContent.none, GUILayout.Width(150));
            }

            if (inventoryLimitProperty != null)
            {
                SerializedProperty inventoryElement = inventoryLimitProperty.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(inventoryElement, GUIContent.none, GUILayout.Width(150));
            }

            if (delayUpgradeProperty != null)
            {
                SerializedProperty delayElement = delayUpgradeProperty.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(delayElement, GUIContent.none, GUILayout.Width(150));
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}

[CustomEditor(typeof(SpeedMovementUpgrade))]
public class SpeedMovementUpgradeEditor : UpgradeArrayEditorBase
{
    private SerializedProperty baseSpeedProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        baseSpeedProperty = serializedObject.FindProperty("baseSpeed");
    }

    protected override void DrawSpecificProperties()
    {
        EditorGUILayout.PropertyField(baseSpeedProperty);
    }
}

[CustomEditor(typeof(CapacityUpgrade))]
public class CapacityUpgradeEditor : UpgradeArrayEditorBase
{
    private SerializedProperty baseInventoryProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        baseInventoryProperty = serializedObject.FindProperty("baseInventoryLimit");
    }

    protected override void DrawSpecificProperties()
    {
        EditorGUILayout.PropertyField(baseInventoryProperty);
    }
}

[CustomEditor(typeof(SpeedActionUpgrade))]
public class SpeedActionUpgradeEditor : UpgradeArrayEditorBase
{
    private SerializedProperty baseDelayProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        baseDelayProperty = serializedObject.FindProperty("baseDelay");
    }

    protected override void DrawSpecificProperties()
    {
        EditorGUILayout.PropertyField(baseDelayProperty);
    }
}
