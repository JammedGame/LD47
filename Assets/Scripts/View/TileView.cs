using TMPro;
using UnityEngine;

public class TileView : MonoBehaviour
{
	[SerializeField] private MeshRenderer meshRenderer = null;
	[SerializeField] private MeshRenderer overlayMeshRenderer = null;
	[SerializeField] private MeshRenderer indicator = null;

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
		if (Tile.TileType == TileType.Undefined)
		{
			meshRenderer.enabled = false;
			return;
		}

		var (tex, rotation, overlay) = Tile.LoadTexture();
		indicator.enabled = Tile.CanChange;
		meshRenderer.enabled = tex;
		overlayMeshRenderer.enabled = overlay;
		meshRenderer.material.mainTexture = tex;
		overlayMeshRenderer.material.mainTexture = overlay;
		meshRenderer.transform.localRotation = Quaternion.Euler(0f, 0f, -rotation.ToAngle());
		meshRenderer.enabled = true;
	}
}