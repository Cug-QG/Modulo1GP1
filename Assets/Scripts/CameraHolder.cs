using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] Transform player;
    void Update()
    {
        //transform.Translate(Vector2.right * Time.deltaTime * speed);
        //player.Translate(Vector2.right * Time.deltaTime * speed);
    }
}
