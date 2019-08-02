using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// from https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483
public class PlayerController : MonoBehaviour
{
    [Header("Variables")]
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float groundDistance = 0.2f;
    public float dashDistance = 5f;
    public LayerMask ground;

    [Header("Components")]
    public new Rigidbody rigidbody;
    public new Transform transform;

    [Header("References")]
    public Transform cameraTransform;

    private Transform groundChecker;
    private bool isGrounded;

    /** Turn private */
    [Header("Public for debugging")]
    public Vector3 inputs;
    public Vector3 newInputs;
    public Vector3 cameraForward;
    public Vector3 inputRotation;
    public Vector3 torqueTest;
    public float torqueInfluence = 0.1f;
    public float result;

    public float TestingNum;
    public Vector3 testingVector;

    public Vector2 mouse = new Vector2();

    private void Start()
    {
        groundChecker = rigidbody.GetComponent<Transform>()?.GetChild(0);
    }

    private void Update()
    {
        inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");
        inputs = Vector3.Normalize(inputs);

        if (inputs == Vector3.zero)
        {
            return;
        }

        // get the direction of the input 
        newInputs = cameraTransform.TransformDirection(inputs);
        newInputs.y = 0f;
        cameraForward = cameraTransform.forward;
        cameraForward.y = 0;
        result = Vector3.SignedAngle(cameraForward, newInputs, Vector3.up);

        // since the camera moves with movement and movement is based on the camera
        // weird things happen when we rotate to 180 since everything can't make up
        // whether to go from the left or right
        if (result > 179f)
        {
            result = 178f;
        }
        else if (result < -179f)
        {
            result = 178f;
        }

        // rotate here!!!
        rigidbody.AddTorque(0f, result * torqueInfluence * Time.deltaTime, 0f, ForceMode.VelocityChange);

        // try moving
        if (inputs.z > 0)
        {
            Vector3 direction = Vector3.Normalize(transform.forward);
            TestingNum = inputs.z * speed * Time.deltaTime;
            testingVector = direction * TestingNum;
            rigidbody.AddForce(testingVector, ForceMode.Impulse);
        }
    }

    //void Update()
    //{
    //    isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, ground, QueryTriggerInteraction.Ignore);


    //    inputs = Vector3.zero;
    //    inputs.x = Input.GetAxis("Horizontal");
    //    inputs.z = Input.GetAxis("Vertical");
    //    if (inputs != Vector3.zero)
    //    {
    //        transform.forward = inputs;
    //    }

    //    if (Input.GetButtonDown("Jump") && isGrounded)
    //    {
    //        rigidbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    //    }
    //    if (Input.GetButtonDown("Dash"))
    //    {
    //        Vector3 dashVelocity = Vector3.Scale(transform.forward, dashDistance *
    //            new Vector3((Mathf.Log(1f / (Time.deltaTime * rigidbody.drag + 1)) / -Time.deltaTime), 0,
    //            (Mathf.Log(1f / (Time.deltaTime * rigidbody.drag + 1)) / -Time.deltaTime)));
    //        rigidbody.AddForce(dashVelocity, ForceMode.VelocityChange);
    //    }
    //}


    //void FixedUpdate()
    //{
    //    rigidbody.MovePosition(rigidbody.position + inputs * speed * Time.fixedDeltaTime);
    //}
}
