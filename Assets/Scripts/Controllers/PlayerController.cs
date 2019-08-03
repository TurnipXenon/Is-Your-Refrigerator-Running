using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// from https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483
public class PlayerController : MonoBehaviour
{
    [Header("Variables")]
    public float speed = 5f;
    public float torqueInfluence = 0.1f;
    public float jumpHeight = 2f;
    public float groundDistance = 0.2f;
    public float dashDistance = 5f;
    public float maximumPitch = 20f;
    public LayerMask ground;

    [Header("Components")]
    public new Rigidbody rigidbody;
    public new Transform transform;
    public Transform stableBody;

    [Header("References")]
    public Transform cameraTransform;

    private Transform groundChecker;
    private bool isGrounded;

    /** Turn private */
    [Header("Public for debugging")]
    public Vector3 inputs;
    public Vector3 relativeInput;
    public Vector3 cameraForward;
    public Vector3 outputSpeed;

    private void Start()
    {
        groundChecker = rigidbody.GetComponent<Transform>()?.GetChild(0);
    }

    private void Update()
    {
        inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");

        if (inputs == Vector3.zero)
        {
            return;
        }

        inputs.Normalize();
        relativeInput = cameraTransform.TransformDirection(inputs);
        relativeInput.y = 0;
        relativeInput.Normalize();

        // rotate here!!!
        outputSpeed = relativeInput * speed * inputs.magnitude * Time.deltaTime;
        rigidbody.AddTorque(Mathf.Clamp(outputSpeed.magnitude, 0f, maximumPitch), 
            inputs.x * torqueInfluence * Time.deltaTime, 0f, ForceMode.VelocityChange);
        rigidbody.AddForce(outputSpeed, ForceMode.Impulse);
    }
}
