using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.5f;
    private Material backgroundMaterial;
    private Vector2 offset;

    private void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        offset.x += scrollSpeed * Time.deltaTime;
        backgroundMaterial.mainTextureOffset = offset;
    }
}