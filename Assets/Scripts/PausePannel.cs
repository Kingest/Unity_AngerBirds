using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePannel : MonoBehaviour
{
    public Animator animr;
    public GameObject Pause;
    public void PauseMenuIn()
    {
        Pause.SetActive(true);
        animr.Play("Pause");
        gameObject.SetActive(false);
        Invoke("Stop", 1.5f);
    }
    public void PauseMenuBack()
    {
       
        animr.Play("PauseBack");
        gameObject.SetActive(true);
        Time.timeScale = 1;
        Invoke("Play", 1.5f);
    }
    void Stop()
    {
        Time.timeScale = 0;
    }
    void Play()
    {
        Pause.SetActive(false);
    }
}
