using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxOffset = -0.15f;

    private new Camera camera;
    private Vector2 startPos;
    private Vector2 travel => (Vector2)camera.transform.position - startPos;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = startPos + travel * parallaxOffset;
    }

}
