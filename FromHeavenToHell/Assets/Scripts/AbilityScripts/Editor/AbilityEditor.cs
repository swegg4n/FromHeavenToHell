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
        projectilePrefab,
        beamPrefab;


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
        projectilePrefab = serializedObject.FindProperty("projectilePrefab");
        beamPrefab = serializedObject.FindProperty("beamPrefab");

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


            AbilityProperty.AbilityType type = (AbilityProperty.AbilityType)abilityType.enumValueIndex;

            switch (type)
            {
                case AbilityProperty.AbilityType.Projectile:
                    EditorGUILayout.PropertyField(projectilePrefab);
                    EditorGUILayout.Space();

                    EditorGUILayout.PropertyField(fireRate);
                    break;

                case AbilityProperty.AbilityType.Beam:
                    EditorGUILayout.PropertyField(beamPrefab);
                    EditorGUILayout.Space();

                    EditorGUILayout.PropertyField(castTime);
                    break;
            }
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(damage);
            EditorGUILayout.PropertyField(speed);
        }

        serializedObject.ApplyModifiedProperties();
    }

    
}
