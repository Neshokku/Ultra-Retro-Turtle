using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * logic.speed * Time.deltaTime);

        if (transform.position.y < -6.5)
        {
            Destroy(gameObject);
        }
    }
}
