using UnityEngine;
using UnityEditor;

namespace Alf.GameGridSystem
{

[CustomEditor(typeof(GameGrid))]
public class GameGridEditor : Editor
{
    private Vector2Int coordinates;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameGrid targetScript = (GameGrid)target;

        GUILayout.Space(10);

        // Text field for entering X and Y coordinates
        GUILayout.Label("Enter Coordinates:");
        coordinates.x = EditorGUILayout.IntField("X:", coordinates.x);
        coordinates.y = EditorGUILayout.IntField("Y:", coordinates.y);

        GUILayout.Space(5);

        // Button to call AddTile function with the entered coordinates
        if (GUILayout.Button("Add Cell"))
        {
            GameObject newCell = targetScript.AddCell(coordinates);
            if (newCell == null)
            {
                Debug.Log("Cell already exists at coordinates: " + coordinates);
            }
            else
            {
                Debug.Log("Cell added at coordinates: " + coordinates);
            }
        }
    }
}
}
