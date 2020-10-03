using System;
using UnityEditor;
using UnityEngine;

public class LevelEditorWindow : EditorWindow
{
	private Vector2 brushScrollPos;
	[NonSerialized] public LevelData Data;
	private Vector2 scrollPos = Vector2.zero;

	private TileType tileBrush;

	public void OnGUI()
	{
		if (Data == null)
		{
			Close();
			return;
		}

		var defaultBrushStyle = new GUIStyle(GUI.skin.button) {padding = new RectOffset(5, 5, 5, 5)};
		var buttonStyle = new GUIStyle(GUI.skin.button) {padding = new RectOffset(0, 0, 0, 0)};

		GUI.Label(new Rect(0, 0, 50, 50), "Brush:");
		var brushes = Enum.GetValues(typeof(TileType));
		var y = 50;
		foreach (var brush in brushes)
		{
			var style = (TileType) brush == tileBrush ? buttonStyle : defaultBrushStyle;
			var brushButton = GUI.Button(new Rect(0, y, 25, 25), ((TileType) brush).LoadTexture().tex, style);
			if (brushButton) tileBrush = (TileType) brush;
			y += 25;
		}

		GUI.Label(new Rect(100, 0, 50, 50), "Level:");
		scrollPos = GUI.BeginScrollView(new Rect(100, 50, 800, 600), scrollPos,
			new Rect(100, 50, 25 * (1 + Data.Width), 25 * (1 + Data.Height)));
		for (var i = 0; i < Data.Tiles.Length; i++)
		{
			var tile = Data.GetTile(i);
			var button = GUI.Button(new Rect(100 + tile.X * 25, 50 + tile.Y * 25, 25, 25), tile.Type.LoadTexture().tex,
				buttonStyle);
			if (button) Data.SetTileType(i, tileBrush);
		}

		GUI.EndScrollView();
	}
}