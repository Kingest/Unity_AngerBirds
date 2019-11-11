using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Birds> birds;//此处List的是游戏物体为GameObject
    public List<Enemy> enemies;
    
    public static GameManager _gameManager;
    private Vector3 OriginPos;//初始的位置
    public GameObject Win;
    public GameObject Lose;
    public GameObject[] Stars;
    private void Awake()
    {
        _gameManager = this;
        if (birds.Count>0)
        {
            OriginPos = birds[0].transform.position;
        }
       
    }
    /// <summary>
    /// 初始化小鸟
    /// </summary>
    private void Initialed()//草了，在Birds中，有行代码是移除birds.this这意味着每次都是从0开始，上一只被移除之后就，下一只生成的小鸟就是i==0号小鸟
    {
        for (int i = 0; i < birds.Count; i++)
        {
            if (i==0)//第一只小鸟
            {
                birds[i].gameObject.transform.position = OriginPos;
                birds[i].enabled = true;
                birds[i].spj2d.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].spj2d.enabled = false;
                
            }
            print(i);
        }
    }
    private void Start()
    {
        Initialed();
    }
    /// <summary>
    /// 判定游戏逻辑
    /// </summary>
    public void NextBirds()
    {
        if (enemies.Count>0)
        {
            if (birds.Count>0)//下一只小鸟准备
            {
                Initialed();
                
            }
            else//输
            {
                Lose.SetActive(true);
            }
        }



        else
        {
            Win.SetActive(true);
           
        }
    }
    public void ShowStar()
    {
        StartCoroutine("Show");
    }
    IEnumerator Show()//用携程方法让星星一颗一颗显示
    {
        for (int i = 0; i < birds.Count+1 ; i++)
        {
            if (i>Stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            Stars[i].SetActive(true);
        }
        
    }

    public void ReStart()
    {
        SceneManager.LoadScene("Start");
    }
}


