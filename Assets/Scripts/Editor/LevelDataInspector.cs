using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelData))]
public class LevelDataInspector : Editor
{
	public LevelData levelData;

	private void OnEnable()
	{
		levelData = target as LevelData;
	}

	private void OnSceneGUI()
	{
		for (var i = 0; i < levelData.Tiles.Length; i++)
		{
			var tileData = levelData.GetTile(i);
		}

		// Gizmos.DrawGUITexture()
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("CAO"))
		{
		}
	}
}