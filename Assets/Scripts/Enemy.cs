using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxspeed = 10;
    public float minspeed = 5;
    private SpriteRenderer sprender;
    public Sprite hurt;
    public GameObject Boom;
    public GameObject hit3000;
    public AudioClip[] EnemyAudio;//获取音乐数组
    public bool isEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        sprender = GetComponent<SpriteRenderer>();
    }

  
    private void OnCollisionEnter2D(Collision2D collision)//都需要rigibody
    {
        if (collision.gameObject.tag=="Player")
        {
            EnemyAudioPlay(EnemyAudio[0]);
            collision.transform.GetComponent<Birds>().BirdHurt();//这个可以说是和虚幻蓝图里的“类型转换为XX蓝图很相似了”
            //通过碰撞调用了Birds脚本中的BirdHurt函数
        }
        if (collision.relativeVelocity.magnitude >= maxspeed)
        {
            Destroy(gameObject);
            Deaded();

        }
        //if (GameManager._gameManager.enemies.Count==0)
        //{
        //    GameManager._gameManager.enemies.RemoveAll();
        //}
       
        else if (collision.relativeVelocity.magnitude < maxspeed && collision.relativeVelocity.magnitude>minspeed)
        {//Vector3.magnitude为Vector的值和Normaled正好相反，后者是方向
           
            sprender.sprite = hurt;//注意是sprintRender组件，而不是sprite
        }
    }
     public void Deaded()
    {
        EnemyAudioPlay(EnemyAudio[0]);
        if (isEnemy)
        {
            GameManager._gameManager.enemies.Remove(this);
        }
        
        Destroy(gameObject);
        gameObject.SetActive(false);
        GameObject bm=  Instantiate(Boom, transform.position, transform.rotation);
        GameObject hit=  Instantiate(hit3000, transform.position + new Vector3(0, 1f, 0), transform.rotation);
        hit.transform.Translate(Vector3.up*Time.deltaTime);
        Destroy(hit, 1);
        
       
    }

    void EnemyAudioPlay(AudioClip clip)//用代码控制播放音乐
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }




    
}
