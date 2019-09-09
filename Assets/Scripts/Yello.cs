using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yello : Birds//继承自birds脚本，继承了全部的属性
{
    /// <summary>
    /// 重写虚方法
    /// </summary>
    public override void ShowSkill()
    {
        base.ShowSkill();

        //获得速度，注意，来自继承类的rigibody组件，前必须有public或者Protect，不可使用private
        rig2d.velocity *= 2;
    }
}
