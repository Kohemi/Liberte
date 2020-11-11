using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class StareWall : MonoBehaviour
{
    public RaycastDetect raycastDetect;

    // 손전등 조정
    public new Transform light;

    // Start is called before the first frame update
    void Start()
    {
        raycastDetect.stare += Interact;
    }

    public void Interact(RaycastHit[] hits)
    {
        if (light == null)
            return;

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Wall"))
            {
                light.LookAt(hit.point);

                // 손전등 각도 조절
                light.Rotate(180f, 0f, 0f);
            }
        }
    }
}
