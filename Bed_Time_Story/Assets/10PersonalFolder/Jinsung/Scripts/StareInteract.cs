using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StareInteract : MonoBehaviour
{
    public string currentTag;
    public RaycastDetect raycastDetect;
    public MoveDestination moveDestination;

    // 손전등 조정
    public new Transform light;

    // 씬 전환 관련
    public float limitTime;
    public PlayableDirector director;
    private float stareTime;
    private bool boat;

    // Start is called before the first frame update
    void Start()
    {
        raycastDetect.stare += Interact;
        raycastDetect.stopStare += StopInteract;

        stareTime = 0;
        boat = false;
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

                        // 손전등 각도 조절
                        light.Rotate(180f, 0f, 0f);
                        break;

                    case "Boat":
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

    public void StopInteract()
    {
        // 응시를 하지 않을 때..
    }
}
