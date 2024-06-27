using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Resource : MonoBehaviour
{
    public enum Ingredients
    {
        TreeLiving,
        DracoWind,
        Crystal,
    }

    public enum Potions
    {
        Regen,
        Power,
        Necromancy,
        Curse
    }
    public enum ResourceType
    {
        Ingredients,
        Potions
    }

    public ResourceType resourceTypeEnum;

    public Ingredients selectedIngredientEnum;
    public Potions selectedPotionEnum;
}

#if UNITY_EDITOR
[CustomEditor(typeof(Resource))]
public class ResourceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Resource resource = (Resource)target;

        resource.resourceTypeEnum = (Resource.ResourceType)EditorGUILayout.EnumPopup("Resource Type", resource.resourceTypeEnum);

        switch (resource.resourceTypeEnum)
        {
            case Resource.ResourceType.Ingredients:
                resource.selectedIngredientEnum = (Resource.Ingredients)EditorGUILayout.EnumPopup("Ingredient", resource.selectedIngredientEnum);
                break;
            case Resource.ResourceType.Potions:
                resource.selectedPotionEnum = (Resource.Potions)EditorGUILayout.EnumPopup("Potion", resource.selectedPotionEnum);
                break;
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
#endif
