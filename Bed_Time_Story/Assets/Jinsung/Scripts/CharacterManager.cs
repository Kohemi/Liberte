using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Transform look;
    public Transform target;
    public Animator animator;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        look.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

        if (Mathf.Abs(look.rotation.eulerAngles.y - transform.rotation.eulerAngles.y) > 30)
        {
            animator.SetTrigger("rotate");
            transform.rotation = look.rotation;
        }
    }
}
