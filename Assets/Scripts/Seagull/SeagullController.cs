using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullController : MonoBehaviour
{
    private LogicScript logic;

    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Movement")]
    [SerializeField] private float verticalSpeedModifier;


    // Start is called before the first frame update
    void Start()
    {
        if (Random.value > 0.5f)
        {
            spriteRenderer.flipY = true;
        }

        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * verticalSpeedModifier * logic.speed * Time.deltaTime);

        if (transform.position.y < -6.5)
        {
            DeleteSeagull();
        }
    }

    private void DeleteSeagull()
    {
        TurtleController turtleTransform = GetComponentInChildren<TurtleController>();

        if (turtleTransform != null)
        {
            turtleTransform.transform.parent = null;
        }
        Destroy(gameObject);
    }
}
