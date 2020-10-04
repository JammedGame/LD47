using TMPro;
using UnityEngine;

public class TileView : MonoBehaviour
{
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private TextMeshPro textMesh;

	public Tile Tile { get; private set; }

	public void UpdateView()
	{
		UpdateCargo();
	}

	public static TileView CreateView(Tile tile, TileView tileViewPrefab)
	{
		var tileView = Instantiate(tileViewPrefab, tile.GetPosition3D(), Quaternion.identity);
		tileView.name = $"Tile[{tile.X}, {tile.Y}]";
		tileView.Tile = tile;
		tileView.UpdateMaterialAndRotation();
		tileView.UpdateCargo();
		return tileView;
	}

	public void UpdateMaterialAndRotation()
	{
		var (tex, rotation) = Tile.LoadTexture();
		meshRenderer.material.mainTexture = tex;
		meshRenderer.transform.localRotation = Quaternion.Euler(0f, 0f, -rotation.ToAngle());
	}

	private void UpdateCargo()
	{
		var cargoCount = Tile.Cargoes.Count;
		textMesh.text = new string('•', cargoCount);
		for (var i = 0; i < cargoCount; i++)
		{
			var color = Tile.Cargoes[i].Color.ToColor();
			ColorSingleCharacter(i, color);
		}
	}

	private void ColorSingleCharacter(int charIndex, Color32 color)
	{
		var meshIndex = textMesh.textInfo.characterInfo[charIndex].materialReferenceIndex;
		var vertexIndex = textMesh.textInfo.characterInfo[charIndex].vertexIndex;
		var vertexColors = textMesh.textInfo.meshInfo[meshIndex].colors32;
		if (vertexColors == null) return;

		vertexColors[vertexIndex + 0] = color;
		vertexColors[vertexIndex + 1] = color;
		vertexColors[vertexIndex + 2] = color;
		vertexColors[vertexIndex + 3] = color;
		textMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
	}
}