using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Raycast : MonoBehaviour
{
    private Vector3 screenCenter;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {
        //Ray가 충돌한 지점의 정보를 저장하는 변수
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        //ray를 100.0f 거리까지 쏘아서 충돌상태 확인
        if(Physics.Raycast(ray, out hit, 100.0f) && gameObject.CompareTag("Doori"))
        {
            Debug.Log(hit);
           
            hit.transform.gameObject.SetActive(false);
            
        }

    }
}
