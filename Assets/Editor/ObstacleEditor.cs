using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleData))]
public class ObstacleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ObstacleData obstacleData = (ObstacleData)target;

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Grid Settings", EditorStyles.boldLabel);

        for (int y = 0; y < 10; y++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int x = 0; x < 10; x++)
            {
                int index = x * 10 + y;
                obstacleData.obstacles[index] = EditorGUILayout.Toggle(obstacleData.obstacles[index]);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}