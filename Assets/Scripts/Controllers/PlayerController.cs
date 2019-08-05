using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

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

    [Header("Variables")]
    public int currentIndex;

    [Header("Components")]
    public Transform characterList;
    public CinemachineFreeLook freeLookCamera;

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

    private CharacterManager mainCharacterManager;
    private new Transform transform;
    private new Rigidbody rigidbody;
    private NavMeshAgent agent;
    private Brain brain;

    private bool isSafeToPossess = false;

    #region Callbacks
    private void Start()
    {
        TryPossessing();
    }

    private void TryPossessing()
    {
        Transform[] gameObjects = characterList.GetComponentsInChildren<Transform>();
        if (gameObjects.Length > 1)
        {
            int offset = 0;
            Brain characterBrain = null;
            while (characterBrain == null && offset < gameObjects.Length)
            {
                int value = ((currentIndex + offset) % (gameObjects.Length - 1)) + 1;
                if (value < 1)
                {
                    value += gameObjects.Length;
                }
                else
                {
                    characterBrain = gameObjects[value].gameObject.GetComponent<Brain>();
                    offset++;
                }
            }

            if (characterBrain != null)
            {
                Possess(characterBrain.gameObject);
                freeLookCamera.Follow = characterBrain.transform;
                freeLookCamera.LookAt = characterBrain.transform;
            }
        }
    }

    private void Possess(int rawInput)
    {
        if (Mathf.Abs(rawInput) != 1)
        {
            return;
        }

        currentIndex += rawInput;
        TryPossessing();
    }

    private void Update()
    {
        if (transform == null)
        {
            isSafeToPossess = false;
            TryPossessing();
        }

        if (isSafeToPossess && Input.GetButtonDown("Possess"))
        {
            isSafeToPossess = false;
            int rawValue = (int)Input.GetAxisRaw("Possess");
            Possess(rawValue);
            return;
        }

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
    #endregion

    private void Possess(GameObject characterObject)
    {
        if (mainCharacterManager != null)
        {
            Depossess();
        }

        mainCharacterManager = characterObject.GetComponentInChildren<CharacterManager>();
        mainCharacterManager.Possess(this);
        brain = characterObject.GetComponent<Brain>();
        agent = characterObject.GetComponent<NavMeshAgent>();
        rigidbody = characterObject.GetComponent<Rigidbody>();
        transform = characterObject.GetComponent<Transform>();

        brain.enabled = false;
        agent.enabled = false;
        rigidbody.isKinematic = false;
        isSafeToPossess = true;
    }

    public void Depossess()
    {
        if (mainCharacterManager == null)
        {
            return;
        }

        agent.enabled = true;
        rigidbody.isKinematic = true;
        brain.enabled = true;

        agent = null;
        brain = null;
        mainCharacterManager = null;
        rigidbody = null;
        transform = null;
    }
}
