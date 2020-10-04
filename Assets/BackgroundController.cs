using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public void Init(LevelData level)
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        transform.position = new Vector3( (level.Width - 1f) / 2, (level.Height - 1f) / -2, 0f);
        transform.localScale = new Vector3(level.Width, level.Height, 0);
        transform.GetComponentInChildren<MeshRenderer>().material.mainTextureScale = new Vector2(level.Width, level.Height);
    }
}
