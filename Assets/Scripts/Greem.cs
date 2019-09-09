using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greem : Birds
{
    /// <summary>
    /// 蓝鸟的技能
    /// </summary>
    public override void ShowSkill()
    {
        //让这个小鸟回旋飞
        base.ShowSkill();//重写的时候自己生成的
        Vector3 speed = rig2d.velocity;
        speed *= -1;
        rig2d.velocity = speed;
    }
}
