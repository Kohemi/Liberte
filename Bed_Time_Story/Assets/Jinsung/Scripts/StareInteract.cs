using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StareInteract : MonoBehaviour
{
    public string currentTag;
    public RaycastDetect raycastDetect;
    public Transform light;

    // Start is called before the first frame update
    void Start()
    {
        raycastDetect.stare += Interact;
        raycastDetect.stopStare += StopInteract;
    }

    public void Interact(RaycastHit hit)
    {
        if(hit.transform.CompareTag(currentTag))
        {
            switch(currentTag)
            {
                case "Wall":
                    light.LookAt(hit.point);
                    break;
            }
        }
    }

    public void StopInteract()
    {
        Debug.Log("See Nothing...");
    }
}
