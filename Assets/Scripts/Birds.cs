using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birds : MonoBehaviour
{
    private bool isclick;
    private GameObject RightPoint;
    private GameObject LeftPoint;
    public float maxdis = 2.5f;
    
    public SpringJoint2D spj2d;
    protected Rigidbody2D rig2d;
    public LineRenderer LineRight;
    public LineRenderer LineLeft;
    public GameObject ExpBoom;
    protected  Trail trail;
    private bool CanMove = true;
    public float smooth = 300;

    public AudioClip[] BirdsAudio;
    private bool isfly = false;
    public Sprite HurtBird;
    public SpriteRenderer sptRd;

    private void Start()
    {
        rig2d = GetComponent<Rigidbody2D>();
        RightPoint = GameObject.Find("RightPoint");
        LeftPoint = GameObject.Find("LeftPoint");
        spj2d = GetComponent<SpringJoint2D>();
        trail = GetComponent<Trail>();
        sptRd = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        
            if (isclick)//鼠标一直按下就需要位置的跟随
            {
                //transform.position = Input.mousePosition;//为什么不能用这个呢？因为这个是以世界坐标为中心，就是屏幕中心，
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//而我们需求的是以相机坐标左下角为0,0

                //到目前为止如果运行游戏，小鸟的Z会和摄像机的Z为一致，这样就看不到了


                //transform.position += new Vector3(0, 0, 10);//第一种方法，如果不写这一句会让小鸟的Z轴变化为何摄像机一样的-10

                transform.position += new Vector3(0, 0, 10);//第二种方法，保证小鸟Z轴不偏移的方法,居然能和上面的坐标兼容？！
                                                            //且不可使用rigibody中的Frozen Z来冻结，已试验确认无效
                transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);//第三种方法
            

                Invoke("Fly", 0.1f);//特别注意这个值0.1不能过大，否则手感很差

                if (Vector3.Distance(transform.position, RightPoint.transform.position) >= maxdis)//进行位置限定，让弹簧距离不超过2.5
                {
                    Vector3 pos = (transform.position - RightPoint.transform.position).normalized;//单位化向量//得到的只有方向
                    pos *= maxdis;//最大长度向量，注意是乘！！！！，方向X长度，得到的就是向量
                    transform.position = pos + RightPoint.transform.position;//再用单位化之后的向量（方向）+长度，就可以得到一个怎么拉都不会出界的小鸟了
                }
                Line();
            }
            
        float posx = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posx, 0, 20), Camera.main.transform.position.y, 
            Camera.main.transform.position.z),smooth*Time.deltaTime);

        if (isfly)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
               
            
        }
        
    }

    private void OnMouseDown()
    {
        if (CanMove)
        {
            Playaudio(BirdsAudio[0]);
            isclick = true;
            rig2d.isKinematic = true;
            LineLeft.enabled = true;
            LineRight.enabled = true;
        }
        
    }
    private void OnMouseUp()
    {
        if (CanMove)
        {
            Playaudio(BirdsAudio[1]);
            isclick = false;
            spj2d.enabled = false;//让小鸟飞出去，只需要在鼠标抬起的时候让这个组件失活（被禁用）即可
                                  //rig2d.isKinematic = true;//第一种方法
                                  //rig2d.bodyType = RigidbodyType2D.Kinematic;//此处设置rigibody为开启动力学，不会让鸟非得太快太低，第二种方法
                                  //!!!!!!! invoke =虚幻中的Delay，注意，没有repeting,只是Invoike
            LineLeft.enabled = false;
            LineRight.enabled = false;
            CanMove = false;
            //鼠标抬起禁用划线组件
        }



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isfly = false;
    }
    private void Fly()
    {
        isfly = true;
        rig2d.isKinematic = false;
        Invoke("NextBird", 5);

        trail.trailStart();
    }
    /// <summary>
    /// 划线的操作
    /// </summary>
    private void Line()
    {
        LineRight.SetPosition(0, RightPoint.transform.position);
        LineRight.SetPosition(1, transform.position);

        LineLeft.SetPosition(0, LeftPoint.transform.position);
        LineLeft.SetPosition(1, transform.position);
    }
    protected virtual void NextBird()//处理下一只小鸟的飞出
    {
        GameManager._gameManager.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(ExpBoom, transform.position, Quaternion.identity);
        GameManager._gameManager.NextBirds();
    }
    public void Playaudio(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
    /// <summary>
    /// 各种类型小鸟的技能
    /// </summary>
    public virtual void ShowSkill()//虚方法，可以在其他代码中重写
    {
        isfly = false;
    }
    public void BirdHurt()
    {
        sptRd.sprite = HurtBird;
    }
    
}
