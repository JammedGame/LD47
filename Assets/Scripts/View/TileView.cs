using UnityEngine;

public class TileView : MonoBehaviour
{
	[SerializeField] private MeshRenderer meshRenderer;

	public Tile Tile { get; private set; }

	public static TileView CreateView(Tile tile, TileView tileViewPrefab)
	{
		var tileView = Instantiate(tileViewPrefab, tile.GetPosition3D(), Quaternion.identity);
		tileView.name = $"Tile[{tile.X}, {tile.Y}]";
		tileView.Tile = tile;
		tileView.UpdateMaterialAndRotation();
		return tileView;
	}

	public void UpdateMaterialAndRotation()
	{
		var (tex, rotation) = Tile.LoadTexture();
		meshRenderer.material.mainTexture = tex;
		transform.localRotation = Quaternion.Euler(0f, 0f, -rotation.ToAngle());
	}
}