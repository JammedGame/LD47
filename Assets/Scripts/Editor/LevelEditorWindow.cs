using System;
using UnityEditor;
using UnityEngine;

public class LevelEditorWindow : EditorWindow
{
	public LevelData Data;

	private TileType tileBrush;

	public void OnGUI()
	{
		GUI.Label(new Rect(0, 0, 50, 50), "Brush:");
		var brushes = Enum.GetValues(typeof(TileType));
		var y = 0;
		foreach (var brush in brushes)
		{
			y += 50;
			var brushButton = GUI.Button(new Rect(0, y, 50, 50), ((TileType) brush).LoadTexture());
			if (brushButton) tileBrush = (TileType) brush;
		}

		GUI.Label(new Rect(100, 0, 50, 50), "Level:");
		for (var i = 0; i < Data.Tiles.Length; i++)
		{
			var tile = Data.GetTile(i);
			var button = GUI.Button(new Rect(100 + tile.X * 50, 50 + tile.Y * 50, 50, 50), tile.Type.LoadTexture());
			if (button) Data.SetTileType(i, tileBrush);
		}
	}
}