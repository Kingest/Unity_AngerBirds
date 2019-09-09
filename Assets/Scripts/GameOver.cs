using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    /// <summary>
    /// 游戏结束，显示星星，要等动画播放完，添加了事件
    /// </summary>
   public void Show()
    {
        GameManager._gameManager.ShowStar();
    }
}
