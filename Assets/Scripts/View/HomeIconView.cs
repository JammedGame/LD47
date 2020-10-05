using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeIconView : MonoBehaviour
{
    public static HomeIconView Spawn(TrainSpawn spawn)
    {
        var prefab = Resources.Load<HomeIconView>("Prefabs/HomeIconView");
        var instance = GameObject.Instantiate(prefab);
        instance.transform.position = TileViewUtil.GetPosition3D(spawn.X, spawn.Y);
        var colorSettings = GameSettings.Instance.GetColorSettings(spawn.Color);
        instance.GetComponentInChildren<MeshRenderer>().material.color = colorSettings.Color;
        return instance;
    }
}
