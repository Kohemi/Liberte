using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StareBoat : MonoBehaviour
{
    public RaycastDetect raycastDetect;

    // 씬 전환 관련
    public float limitTime;
    public PlayableDirector director;
    private float stareTime;
    private bool boat;

    // Start is called before the first frame update
    void Start()
    {
        raycastDetect.stare += Interact;

        stareTime = 0;
        boat = false;
    }

    public void Interact(RaycastHit[] hits)
    {
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Boat"))
            {
                if (boat) break;

                stareTime += Time.deltaTime;
                if (stareTime >= limitTime)
                {
                    director.Play();
                    boat = true;
                }
                break;
            }
        }
    }
}