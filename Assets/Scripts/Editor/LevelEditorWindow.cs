using UnityEditor;
using UnityEngine;

public class LevelEditorWindow : EditorWindow
{
	private Vector2 brushScrollPos = Vector2.zero;
	private Vector2 scrollPos = Vector2.zero;
	private TileType tileBrush;

	public LevelData Data { get; set; }

	public void OnGUI()
	{
		if (Data == null)
		{
			Close();
			return;
		}

		var defaultBrushStyle = new GUIStyle(GUI.skin.box) {padding = new RectOffset(3, 3, 3, 3)};
		var activeBrushStyle = new GUIStyle(GUI.skin.box) {padding = new RectOffset(0, 0, 0, 0)};
		var tileStyle = new GUIStyle(GUI.skin.box) {padding = new RectOffset(0, 0, 0, 0)};

		GUI.Label(new Rect(0, 0, 50, 50), "Brush:");
		var brushes = GameSettings.Instance.SettingsPerType;
		var y = 50;
		brushScrollPos = GUI.BeginScrollView(new Rect(0, y, 50, 600), brushScrollPos,
			new Rect(0, y, 50, 25 * (1 + brushes.Count)));
		foreach (var brush in brushes)
		{
			var style = brush.TileType == tileBrush ? activeBrushStyle : defaultBrushStyle;
			var brushPos = new Rect(0, y, 25, 25);
			var (tex, rotate) = brush.TileType.LoadTexture();
			GUIUtility.RotateAroundPivot(rotate.ToAngle(), brushPos.center);
			var brushButton = GUI.Button(brushPos, tex, style);
			if (brushButton) tileBrush = brush.TileType;

			GUI.matrix = Matrix4x4.identity;
			y += 25;
		}

		GUI.EndScrollView();

		GUI.Label(new Rect(100, 0, 50, 50), "Level:");
		scrollPos = GUI.BeginScrollView(new Rect(100, 50, 800, 600), scrollPos,
			new Rect(100, 50, 25 * (1 + Data.Width), 25 * (1 + Data.Height)));
		for (var i = 0; i < Data.Tiles.Length; i++)
		{
			var tile = Data.GetTile(i);
			var buttonPos = new Rect(100 + tile.X * 25, 50 + tile.Y * 25, 25, 25);
			var (tex, rotate) = tile.Type.LoadTexture();
			GUIUtility.RotateAroundPivot(rotate.ToAngle(), buttonPos.center);
			var button = GUI.Button(buttonPos, tex, tileStyle);
			if (button)
			{
				Data.SetTileType(i, tileBrush);
				EditorUtility.SetDirty(Data);
			}

			GUI.matrix = Matrix4x4.identity;
		}

		GUI.EndScrollView();
	}
}