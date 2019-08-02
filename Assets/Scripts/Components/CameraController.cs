using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private const string INPUT_MOVE_CAMERA = "MoveCamera";
    private const string INPUT_MOUSE_X = "MoveCamera";
    private const string INPUT_MOUSE_Y = "MoveCamera";

    // from https://gamedev.stackexchange.com/questions/104693/how-to-use-input-getaxismouse-x-y-to-rotate-the-camera
    public float speedH = 2f;
    public float speedV = 2f;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineTransposer transposer;

    private float yaw = 0f;
    private float pitch = 0f;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        transposer = GetComponent<CinemachineTransposer>();
    }

    private void Update()
    {
        if (Input.GetButton(INPUT_MOVE_CAMERA))
        {
            yaw += speedH * Input.GetAxis(INPUT_MOUSE_X);
            pitch += speedV * Input.GetAxis(INPUT_MOUSE_Y);
        }
    }
}
