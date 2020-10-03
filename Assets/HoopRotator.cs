using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopRotator : MonoBehaviour
{

    public Transform player;
    public float Speed;
    public float RotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, RotationSpeed);

        var translation = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        translation *= Speed * Time.deltaTime;
        player.Translate(translation);
    }
}
