using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleHitboxController : MonoBehaviour
{
    private TurtleController baseTurtle;
    private float shellSpeedModifier = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        baseTurtle = GetComponentInParent<TurtleController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10) {
            baseTurtle.turtleGameOver(collision.transform);
        }

        if (collision.gameObject.layer == 9)
        {
            baseTurtle.movementSpeedModifier *= shellSpeedModifier;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            baseTurtle.movementSpeedModifier /= shellSpeedModifier;
        }
    }
}
