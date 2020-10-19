using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorController : MonoBehaviour
{
    public PlayableDirector cut1;
    public PlayableDirector cut2;
    public PlayableDirector cut3;
    public PlayableDirector cut4;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            cut1.Play();
        }
        
    }
}
