using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoIconsView : MonoBehaviour
{
    public CargoSpawner Spawner { get; private set; }
    public MeshRenderer[] CargoIcons;

    public static CargoIconsView Create(CargoSpawner spawner)
    {
        var prefab = Resources.Load<CargoIconsView>("Prefabs/CargoIconsView");
        var newSpawner = GameObject.Instantiate(prefab, spawner.Tile.GetPosition3D(), Quaternion.identity);
        newSpawner.Spawner = spawner;
        return newSpawner;
    }

    public void UpdateIcons()
    {
    }
}
