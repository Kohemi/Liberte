using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDestination : MonoBehaviour
{
    public float speed;
    public Vector3 rotate;
    public Vector3 translate;
    public Transform destination;

    // Use this for initialization
	void Start ()
    {
        transform.LookAt(destination);
        transform.Rotate(rotate);
    }

    public void Move()
    {
        transform.Translate(translate * speed);
    }
}
