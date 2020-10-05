using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] float period = 0f;
    [SerializeField] Texture[] sprites = null;
    [SerializeField] MeshRenderer meshRenderer = null;

    float timer;
    int index;

    void Update()
    {
        timer += Time.deltaTime;
        while (timer > period)
        {
            index++;
            meshRenderer.material.mainTexture = sprites[index % sprites.Length];
            timer -= period;
        }
    }
}
