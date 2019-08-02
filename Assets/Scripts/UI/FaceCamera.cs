using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private new Camera camera;

    private void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas)
        {
            camera = canvas.worldCamera;
        }
    }

    private void Update()
    {
        transform.rotation = camera.transform.rotation;
    }
}
