using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    public Tile Tile { get; private set; }

    public static TileView CreateView(Tile tile, TileView tileViewPrefab)
    {
        var tileView = GameObject.Instantiate(tileViewPrefab, tile.GetPosition3D(), Quaternion.identity);
        tileView.name = $"Tile[{tile.X}, {tile.Y}]";
        tileView.Tile = tile;
        tileView.UpdateMaterial();
        return tileView;
    }

    public void UpdateMaterial()
    {
        meshRenderer.material.mainTexture = null;
    }
}