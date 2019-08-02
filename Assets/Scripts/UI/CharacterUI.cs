using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/** Attach on canvas */
[RequireComponent(typeof(Canvas))]
public class CharacterUI : MonoBehaviour
{
    public Vector3 offset;

    public Transform follow;
    public PlayerController playerController;

    public TextMeshProUGUI textDetail;

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
        transform.position = follow.position + offset;
        transform.rotation = camera.transform.rotation;
        textDetail.text = playerController.inputs.ToString();
    }
}
