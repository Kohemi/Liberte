using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabInteract : MonoBehaviour
{
    public DistanceGrabbable distanceGrabbable;
    public ParticleSystem effect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || !distanceGrabbable.grabbedBy)
        {
            //effect.Stop();
            return;
        }

        effect.Play();
    }
}
