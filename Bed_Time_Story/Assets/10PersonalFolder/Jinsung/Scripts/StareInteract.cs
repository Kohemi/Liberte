using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StareInteract : MonoBehaviour
{
    public string currentTag;
    public RaycastDetect raycastDetect;
    public MoveDestination moveDestination;

    // 천장 주시
    public new Transform light;

    // 천장 가운데 주시
    public float moveTime;
    private float stareTime; 

    // Start is called before the first frame update
    void Start()
    {
        raycastDetect.stare += Interact;
        raycastDetect.stopStare += StopInteract;

        stareTime = 0;
    }

    public void Interact(RaycastHit[] hits)
    {
        foreach(RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag(currentTag))
            {
                switch (currentTag)
                {
                    case "Wall":
                        light.LookAt(hit.point);
                        break;

                    case "Center":
                        Debug.Log("Look Center");

                        stareTime += Time.deltaTime;
                        if (stareTime >= moveTime)
                            moveDestination.Move();
                        break;
                }
            }
        }
    }

    public void StopInteract()
    {
        // 응시를 하지 않을 때..
    }
}
