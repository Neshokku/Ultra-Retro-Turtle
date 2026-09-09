using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    private LogicScript logic;
    public Rigidbody2D myRb;
    private Transform playerPos;
    public SpriteRenderer render;
    private bool reachedPlayer = false;


    [SerializeField] private GameObject seagull;
    private float seagullYSpawn = 7f;

    public AnimationCurve curve;

    public float horizontalSpeed = 0.5f;

    public float verticalSpeedModifier = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        playerPos = logic.playerController.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= playerPos.position.y) 
        {
            if (!reachedPlayer)
            {
                reachedPlayer = true;
                spawnSeagull();
                StartCoroutine(fadeOut());
            }
        } 
        else
        {
            transform.Translate(Vector3.down * verticalSpeedModifier * logic.speed * Time.deltaTime);
            if (transform.position.x < playerPos.position.x)
            {
                myRb.velocity = new Vector2(horizontalSpeed, myRb.velocity.y);
            }
            else if (transform.position.x > playerPos.position.x)
            {
                myRb.velocity = new Vector2(-(horizontalSpeed), myRb.velocity.y);
            }
        }
    }

    private void spawnSeagull()
    {
        Instantiate(seagull, new Vector3(transform.position.x, seagullYSpawn, transform.position.z), Quaternion.Euler(0, 0, -90));
    }

    IEnumerator fadeOut()
    {
        float fadeTime = 1.0f;
        float elapsedTime = 0.0f;
        float startA = render.color.a;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;

            render.color = new Color(render.color.r, render.color.g, render.color.b, Mathf.Lerp(startA, 0.0f, curve.Evaluate(elapsedTime / fadeTime)));
            yield return null;
        }

        Destroy(gameObject);
    }
}
