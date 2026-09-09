using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{
    public Rigidbody2D myRb;
    public LogicScript logic;
    public float movementSpeed;
    public bool canMove = false;
    private bool watched = false;
    private Transform playerPos;
    public Animator animator;
    private float canMoveProbability = 0.4f;

    private Coroutine moveTowardsPlayerCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        playerPos = logic.playerController.transform;
        if (Random.value < canMoveProbability)
        {
            canMove = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("VelocityX", Mathf.Abs(myRb.velocity.x));

        if (canMove && !watched && transform.position.y <= 3.5f)
        {
            animator.SetTrigger("WatchAnimation");
            watched = true;
        }

        if (canMove && playerInRange()) 
        {
            canMove = false;
            moveTowardsPlayerCoroutine = StartCoroutine(moveTowardsPlayer());
        }

        transform.Translate(Vector3.down * logic.speed * Time.deltaTime);

        if (transform.position.y < -6.5)
        {
            DeleteCrab();
        }
    }

    private void DeleteCrab()
    {
        TurtleController turtleTransform = GetComponentInChildren<TurtleController>();

        if (turtleTransform != null)
        {
            turtleTransform.transform.parent = null;
        }
        Destroy(gameObject);
    }

    bool playerInRange()
    {
        if (transform != null && transform.position.y - logic.speed < playerPos.position.y)
        {
            return true;
        }

        return false;
    }

    IEnumerator moveTowardsPlayer()
    {
        float elapsedTime = 0.0f;
        float duration = 1.0f;
        int walkDirection;

        if (transform.position.x < playerPos.position.x) { 
            walkDirection = 1;
        }
        else
        {
            walkDirection = -1;
        }

        while (elapsedTime < duration) {

            myRb.velocity = new Vector2(walkDirection * movementSpeed, myRb.velocity.y);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        myRb.velocity = Vector3.zero;
    }

    public void CrabStop()
    {
        if (canMove)
        {
            canMove = false;
        }
        if (moveTowardsPlayerCoroutine != null)
        {
            StopCoroutine(moveTowardsPlayerCoroutine);
        }
        myRb.velocity = Vector3.zero;
    }
}
