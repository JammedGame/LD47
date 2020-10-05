using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoIconsView : MonoBehaviour
{
    public Color DefaultColor = Color.black;
    public CargoSpawner Spawner { get; private set; }
    public MeshRenderer[] CargoIcons;

    public static CargoIconsView Create(CargoSpawner spawner)
    {
        var prefabPath = GetPrefabPath(spawner);
        var prefab = Resources.Load<CargoIconsView>(prefabPath);
        var newSpawner = GameObject.Instantiate(prefab, spawner.Tile.GetPosition3D(), Quaternion.identity);
        newSpawner.Spawner = spawner;
        newSpawner.UpdateIcons();
        newSpawner.Spawner.OnUpdate += newSpawner.UpdateIcons;
        return newSpawner;
    }

	private static string GetPrefabPath(CargoSpawner spawner)
	{
        switch(spawner.CargoToBeSpawned.Count)
        {
            case 1: return "Prefabs/CargoIconsView_1";
            case 2: return "Prefabs/CargoIconsView_2";
            case 3: return "Prefabs/CargoIconsView_4";
            default: return "Prefabs/CargoIconsView_4";
        }
	}

	public void UpdateIcons()
    {
		for (int i = 0; i < CargoIcons.Length; i++)
        {
			MeshRenderer icon = CargoIcons[i];
			var hasCargo = Spawner.Cargos.Count > i;
            if (!hasCargo)
            {
                icon.material.color = DefaultColor;
            }
            else
            {
                var cargo = Spawner.Cargos[i];
                icon.material.color = GameSettings.Instance.GetColorSettings(cargo.Color).Color;
            }
        }
    }
}
