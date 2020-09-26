using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StareInteract : MonoBehaviour
{
    private string currentTag;

    public RaycastDetect raycastDetect;

    // Start is called before the first frame update
    void Start()
    {
        currentTag = transform.tag;
        raycastDetect.stare += Interact;
        raycastDetect.stopStare += StopInteract;
    }

    public void Interact(RaycastHit hit)
    {
        switch(currentTag)
        {
            case "Turtle":

                if(hit.transform.CompareTag(currentTag))
                    Debug.Log("See Turtle...");
                
                break;

            default:
                break;
        }
    }

    public void StopInteract()
    {
        Debug.Log("See Nothing...");
    }
}