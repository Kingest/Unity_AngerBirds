using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black : Birds
{
    public GameObject Exp;
    public List<Enemy> block = new List<Enemy>();
    public override void ShowSkill()
    {
        if (block != null&&block.Count>0)
        {
            for (int i = 0; i < block.Count; i++)
            {
                block[i].Deaded();
            }
        }
        base.ShowSkill();
        clear();
    }
    
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag=="Enemy")
    //    {
    //        Destroy(collision.gameObject);
    //    }
    //}

    /// <summary>
    /// 进入触发区域
    /// </summary>
    /// <param name="collision"></param>
    /// 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            //GameObject Gb=  Instantiate(Exp, transform.position, Quaternion.identity);
            //Gb.transform.localScale = new Vector3(2, 2, 2);
            //Destroy(collision.gameObject);
            //Invoke("Delay", 0.1f);
            block.Add(collision.gameObject.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        block.Remove(collision.gameObject.GetComponent<Enemy>());
    }
    void clear()
    {
        rig2d.velocity = Vector3.zero;
        Instantiate(ExpBoom, transform.position, Quaternion.identity);
        sptRd.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        trail.cleanTrail();
    }
    protected override void NextBird()
    {
        GameManager._gameManager.birds.Remove(this);
        Destroy(gameObject);
        
        GameManager._gameManager.NextBirds();
    }
    //void Delay()
    //{
    //    Destroy(gameObject);

    //}


}
