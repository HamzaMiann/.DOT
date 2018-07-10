using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{

    public GameObject menu; // Assign in inspector
    private bool isShowing;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
    }

    public void hide()
    {
        isShowing = false;
        menu.SetActive(false);
        GetComponent<AudioSource>().Play();
    }

    public void exit()
    {
        Application.Quit();
    }
}