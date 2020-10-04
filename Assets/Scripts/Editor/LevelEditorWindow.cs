using UnityEditor;
using UnityEngine;

public class LevelEditorWindow : EditorWindow
{
	private Vector2 brushScrollPos = Vector2.zero;
	private bool magnifierActive = true;
	private Vector2 magnifierPos;
	private bool pipetteActive;
	private Vector2 scrollPos = Vector2.zero;

	private TileType tileBrush;

	private TileType TileBrush
	{
		get => tileBrush;
		set
		{
			tileBrush = value;
			magnifierActive = pipetteActive = false;
		}
	}

	public LevelData Data { get; set; }

	private void OnEnable()
	{
		Undo.undoRedoPerformed += Repaint;
	}

	public void OnGUI()
	{
		if (Data == null)
		{
			Close();
			return;
		}

		var defaultBrushStyle = new GUIStyle(GUI.skin.box)
		{
			padding = new RectOffset(3, 3, 3, 3)
		};
		var activeBrushStyle = new GUIStyle(GUI.skin.box)
		{
			padding = new RectOffset(3, 3, 3, 3), normal = new GUIStyleState
			{
				background = Resources.Load<Texture2D>("Textures/EditorButtonBorder")
			}
		};
		var tileStyle = new GUIStyle(GUI.skin.box)
		{
			padding = new RectOffset(0, 0, 0, 0)
		};
		var x = 0;
		var y = 0;

		var magnifier = GUI.Button(new Rect(x, y, 50, 50), Resources.Load<Texture>("Textures/Magnifier"),
			magnifierActive ? activeBrushStyle : defaultBrushStyle);
		if (magnifier)
		{
			magnifierActive = true;
			pipetteActive = false;
		}

		x += 50;
		GUI.Label(new Rect(x, y, 100, 25), magnifierPos.ToString());

		x = 0;
		y += 50;
		var pipette = GUI.Button(new Rect(x, y, 50, 50), Resources.Load<Texture>("Textures/Pipette"),
			pipetteActive || Event.current.alt ? activeBrushStyle : defaultBrushStyle);
		if (pipette)
		{
			magnifierActive = false;
			pipetteActive = true;
		}

		y += 75;
		var brushes = GameSettings.Instance.SettingsPerType;
		GUI.Label(new Rect(x, y, 50, 25), "Brush:");
		y += 25;
		brushScrollPos = GUI.BeginScrollView(new Rect(x, y, 225, 600), brushScrollPos,
			new Rect(x, y, 200, 13 * (1 + brushes.Count)));
		for (var i = 0; i < brushes.Count; i++)
		{
			var brush = brushes[i];
			var style = !magnifierActive && !pipetteActive && brush.TileType == TileBrush
				? activeBrushStyle
				: defaultBrushStyle;
			var brushPos = new Rect(x, y, 50, 50);
			var (tex, rotate, overlay) = brush.TileType.LoadTexture();
			GUIUtility.RotateAroundPivot(rotate.ToAngle(), brushPos.center);
			var brushButton = GUI.Button(brushPos, tex, style);
			if (brushButton) TileBrush = brush.TileType;

			GUI.matrix = Matrix4x4.identity;
			if (i % 4 == 3)
			{
				x = 0;
				y += 50;
			}
			else
			{
				x += 50;
			}
		}

		GUI.EndScrollView();

		x = 275;
		y = 125;
		GUI.Label(new Rect(x, y, 50, 25), "Level:");
		y += 25;
		scrollPos = GUI.BeginScrollView(new Rect(x, y, 800, 600), scrollPos,
			new Rect(x, y, 50 * (1 + Data.Width), 50 * (1 + Data.Height)));
		for (var i = 0; i < Data.Tiles.Length; i++)
		{
			var tile = Data.GetTile(i);
			var buttonPos = new Rect(x + tile.X * 50, y + tile.Y * 50, 50, 50);
			var (tex, rotate, overlay) = tile.Type.LoadTexture();
			GUIUtility.RotateAroundPivot(rotate.ToAngle(), buttonPos.center);
			var button = GUI.Button(buttonPos, tex, tileStyle);
			if (button)
			{
				if (pipetteActive || Event.current.alt)
				{
					TileBrush = tile.Type;
				}
				else if (magnifierActive)
				{
					magnifierPos = new Vector2(tile.X, tile.Y);
				}
				else
				{
					Undo.RegisterCompleteObjectUndo(Data, "Set Tile Type");
					Data.SetTileType(i, TileBrush);
					EditorUtility.SetDirty(Data);
				}
			}

			GUI.matrix = Matrix4x4.identity;
		}

		GUI.EndScrollView();
	}
}