using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabHitboxController : MonoBehaviour
{
    private CrabController baseCrab;

    // Start is called before the first frame update
    void Start()
    {
        baseCrab = GetComponentInParent<CrabController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            baseCrab.CrabStop();
        }
    }
}
