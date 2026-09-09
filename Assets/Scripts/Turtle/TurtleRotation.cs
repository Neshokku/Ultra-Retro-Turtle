using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleRotation : MonoBehaviour
{
    TurtleController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<TurtleController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (controller.turtleIsAlive) 
        {
            float targetRotationZ = Mathf.Clamp(-(controller.myRb.velocity.x) * 10, -30, 30);
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetRotationZ);

            transform.rotation = targetRotation;
        }
    }
}
