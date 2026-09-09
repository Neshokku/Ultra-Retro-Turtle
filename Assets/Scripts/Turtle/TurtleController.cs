using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurtleController : MonoBehaviour
{
    public Rigidbody2D myRb;
    public CameraShake cameraShake;
    public Animator animator;

    public float movementSpeed;
    public float movementSpeedModifier = 1;

    public bool canMove = true;

    public float leapMultiplier;

    public float doublePressTime;
    public float lastPress = -1f;

    public bool turtleIsAlive = true;
    public LogicScript logic;

    private PlayerInput playerInput;
    private float inputMovement;
    private float lastLeapInput;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        inputMovement = playerInput.actions["Move"].ReadValue<float>();

        if (canMove && turtleIsAlive)
        {
            myRb.velocity = new Vector2(inputMovement * ModifiedSpeed(), myRb.velocity.y);
        }

    }

    private void FixedUpdate()
    {

    }

    float ModifiedSpeed()
    {
        return movementSpeed * movementSpeedModifier;
    }

    public void LeapActionLeft(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && canMove && turtleIsAlive)
        {
            StartCoroutine(Leap(-1));
        }
    }

    public void LeapActionRight(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && canMove && turtleIsAlive)
        {
            StartCoroutine(Leap(1));
        }
    }

    IEnumerator Leap(float direction)
    {
        canMove = false;

        myRb.velocity = new Vector2(direction * movementSpeed * leapMultiplier, myRb.velocity.y);

        yield return new WaitForSeconds(0.8f);

        canMove = true;
    }

    public void turtleGameOver(Transform captor)
    {
        if (turtleIsAlive)
        {
            turtleIsAlive = false;

            StartCoroutine(cameraShake.Shake(.3f, 0.5f));
            logic.gameOver();
            myRb.velocity = Vector3.zero;
            animator.speed = 0.0f;
            transform.parent = captor;
        }
    }
}
