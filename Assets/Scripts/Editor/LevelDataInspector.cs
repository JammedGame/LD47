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

	public override void OnInspectorGUI()
	{
		if (GUILayout.Button("Level Editor"))
		{
			var window = EditorWindow.GetWindow<LevelEditorWindow>(levelData.name);
			window.Data = levelData;
			window.Show();
		}

		base.OnInspectorGUI();
	}
}