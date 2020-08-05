using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinHead : MonoBehaviour
{

    //2D触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PinHead")//判断标签是不是PinHead
        {

            //查找到引擎中的GameManager，获取Gamemanager组件，调用Gameover方法
            GameObject.Find("GameManager").GetComponent<Gamemanager>().Gameover();

        }
    }



}
