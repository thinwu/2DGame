using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public float clampLeft;
    public float clampRight;
    public float clampUp;
    public float clampDown;

    private float cameraY;

    // Use this for initialization
    void Start()
    {
        cameraY = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < clampRight)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if ( transform.position.x > clampLeft)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (transform.position.y != cameraY)
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
    }
}
