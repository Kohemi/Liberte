using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayer : MonoBehaviour
{
    public PlayableDirector cut1; 
    public PlayableDirector cut2;
    public GameObject player;
    public bool isCut1Ended;
    public bool isStarted;

    // Start is called before the first frame update
    void Start()
    {
     
   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            isCut1Ended = true;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            isStarted = true;
        }

        if(isStarted == true)
        {
            cut1.Play();
            Invoke("Cut1End", 5f);
            

        }

        if(isCut1Ended == true)
        {
            cut2.Play();
        }
    }

    public void StartTimeline()
    {
        cut1.Play();
    }

    void Cut1End()
    {
        isCut1Ended = true;
    }

}
