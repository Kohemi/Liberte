using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KidRoomMananger : MonoBehaviour
{
    public GameObject dark;
    public GameObject original;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadBeach()
    {
        SceneManager.LoadScene("Level2_Beach");
    }

    public void SetDark()
    {
        dark.SetActive(true);
        original.SetActive(false);
    }

    public void SetOriginal()
    {
        original.SetActive(true);
        dark.SetActive(false);
    }
}