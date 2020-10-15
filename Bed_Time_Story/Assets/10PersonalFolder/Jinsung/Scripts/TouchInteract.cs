using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInteract : MonoBehaviour
{
    public RaycastDetect raycastDetect;

    public bool[] titleTouches;

    // 터치 시간 체크
    public float limitTime;
    private DateTime startTime;

    // 임시 실험
    public TextMesh test1;
    
    // Start is called before the first frame update
    void Start()
    {
        raycastDetect.stare += Interact;
        raycastDetect.stopStare += StopInteract;

        for (int i = 0; i < titleTouches.Length; i++)
            titleTouches[i] = false;
    }

    public void Interact(RaycastHit[] hits)
    {
        foreach (RaycastHit hit in hits)
        {
            switch (hit.transform.tag)
            {
                case "Title1":
                    titleTouches[0] = true;
                    titleTouches[1] = false;
                    startTime = DateTime.Now;
                    break;

                case "Title2":
                    if (!titleTouches[0] || titleTouches[1]) break;

                    titleTouches[1] = true;
                    break;

                case "Title3":
                    if (!titleTouches[0] || !titleTouches[1]) break;

                    CheckTimeDiff();
                    break;

                default:
                    break;
            }
        }
    }

    public void CheckTimeDiff()
    {
        TimeSpan timeDiff = DateTime.Now - startTime;
        Debug.Log("Time: " + timeDiff.Seconds);
        if (timeDiff.Seconds < limitTime)
        {
            Debug.Log("Success");

            // 임시 실험
            test1.color = Color.blue;
        }
        else
        {
            for (int i = 0; i < titleTouches.Length; i++)
                titleTouches[i] = false;
        }
    }

    public void StopInteract()
    {
        // 터치를 안할 때
    }
}