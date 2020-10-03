using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelData))]
public class LevelDataInspector : Editor
{
	public LevelData levelData;

	private void OnEnable()
	{
		levelData = target as LevelData;
		SceneView.duringSceneGui += DuringSceneGUI;
	}

	private void OnDisable()
	{
		SceneView.duringSceneGui -= DuringSceneGUI;
	}

	private void DuringSceneGUI(SceneView sceneView)
	{
		for (var i = 0; i < levelData.Tiles.Length; i++)
		{
			var tileData = levelData.GetTile(i);
			Graphics.DrawTexture(new Rect(tileData.X, tileData.Y, 1, 1), tileData.Type.LoadTexture());
		}
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("CAO"))
		{
		}
	}
}