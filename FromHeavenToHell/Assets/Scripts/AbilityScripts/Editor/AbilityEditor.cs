using UnityEditor;

[CustomEditor(typeof(AbilityProperty)), CanEditMultipleObjects]
public class AbilityEditor : Editor
{

    private SerializedProperty
        icon,
        color,
        abilityType,
        damage,
        speed,
        fireRate,
        castTime,
        prefab;


    private void OnEnable()
    {
        //Setting up SerializeObject
        icon = serializedObject.FindProperty("icon");
        color = serializedObject.FindProperty("color");
        abilityType = serializedObject.FindProperty("abilityType");
        damage = serializedObject.FindProperty("damage");
        speed = serializedObject.FindProperty("speed");
        fireRate = serializedObject.FindProperty("fireRate");
        castTime = serializedObject.FindProperty("castTime");
        prefab = serializedObject.FindProperty("prefab");
    }
    

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //Adding PropertyFields
        {
            EditorGUILayout.PropertyField(icon);
            EditorGUILayout.PropertyField(color);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(abilityType);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(damage);
            EditorGUILayout.PropertyField(speed);


            AbilityProperty.AbilityType type = (AbilityProperty.AbilityType)abilityType.enumValueIndex;

            switch (type)
            {
                case AbilityProperty.AbilityType.Projectile:
                    EditorGUILayout.PropertyField(prefab);
                    EditorGUILayout.PropertyField(fireRate);
                    break;

                case AbilityProperty.AbilityType.Ray:
                    EditorGUILayout.PropertyField(castTime);
                    break;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    
}
