using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetect : MonoBehaviour
{
    private RaycastHit hit;

    public float maxDistance;

    // 응시할 때 발동
    public delegate void Stare(RaycastHit hit);
    public Stare stare;

    // 응시안할 때 발동
    public delegate void StopStare();
    public StopStare stopStare;

    // Update is called once per frame
    void Update()
    {
        // 응시 범위 확인
        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.blue, 0.3f);

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
            stare(hit);
        else
            stopStare();
    }
}
