using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePannel : MonoBehaviour
{
    public Animator animr;
    public GameObject Pause;
    private bool isplaying = false;
    public void PauseMenuIn()
    {
        if (isplaying==false)
        {
            Pause.SetActive(true);
            animr.Play("Pause");
            gameObject.SetActive(false);
            Invoke("Stop", 1f);
            isplaying = true;
        }
       
    }
    public void PauseMenuBack()
    {
        if (isplaying==true)
        {
            Time.timeScale = 1;
            animr.Play("PauseBack");
            gameObject.SetActive(true);

            Invoke("Play", 1f);
            isplaying = false;
        }
        
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
