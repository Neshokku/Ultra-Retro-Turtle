using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandScroller : MonoBehaviour
{
    public LogicScript logic;
    public float startingYPos;
    public float resetYPos; 

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        ResetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * logic.speed * Time.deltaTime);

        if (transform.position.y <= resetYPos)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = new Vector3(transform.position.x, startingYPos, transform.position.z);
    }
}
