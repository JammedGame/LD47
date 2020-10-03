using UnityEditor;
using UnityEngine;

public class LevelEditorWindow : EditorWindow
{
	public LevelData Data;

	public void OnGUI()
	{
		for (var i = 0; i < Data.Tiles.Length; i++)
		{
			var tile = Data.GetTile(i);
			var button = GUI.Button(new Rect(tile.X * 50, tile.Y * 50, 50, 50), tile.Type.LoadTexture());
			// if (button) EditorGUILayout.EnumPopup()
		}
	}
}