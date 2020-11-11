using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
    public float ySpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 octYPos = transform.position;

        if (transform.position.y >= 2.0f)
        {
            ySpeed *= -1;
        }
        
        if(transform.position.y <= -2.0f)
        {
            ySpeed *= -1;
        }


        octYPos.y += ySpeed;
        transform.position = octYPos;

    }
}
