using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellRotation : MonoBehaviour
{
    private float rotationZ;

    // Start is called before the first frame update
    void Start()
    {
        rotationZ = Random.Range(0.0f, 360.0f);

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
