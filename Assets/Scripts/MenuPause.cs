using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public GameObject MenuPanel;
    public bool isMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenu)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        MenuPanel.SetActive(true);
        Time.timeScale = 0;
        isMenu = true;
    }
    public void Continue()
    {
        MenuPanel.SetActive(false);
        Time.timeScale = 1;
        isMenu = false;
    }
}
