using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public float speed = 5;  //速度
    private bool isFly = false;  //是否在飞行状态
    private bool isReach = false;  //针是否从spawn位置到达start位置

    private Vector3 targetCirlePos; //针到圆边的距离
    private Transform start;
    private Transform circle;


    // Start is called before the first frame update
    void Start()
    {
        start = GameObject.Find("Start").transform;//获取引擎中Start组件
        circle = GameObject.Find("Circle").transform;//获取引擎中Circle组件
        targetCirlePos = circle.position;//圆的位置
        targetCirlePos.y -= 1.3f;//圆的位置的y轴自减1.3  得到的targetCirlePos直（0，原先的值-1.3f，0）
    }

    // Update is called once per frame
    void Update()
    {
        if (isFly == false)//整体判断针是否已经到达start位置
        {
            if (isReach == false)
            {

                //movetowords方法（当前位置移动到目标位置）的三个参数（当前位置，目标位置 ，速度）
                transform.position = Vector3.MoveTowards(transform.position, start.position, speed * Time.deltaTime);
                //distance方法，当前位置到目标位置的距离，判断是否小于0.05f，就是判断是否到达目标位置
                if (Vector3.Distance(transform.position, start.position) < 0.05f)
                {
                    isReach = true;
                }

            }

        }
        else
        {
            //针当前位置到达圆的位置，速度不变
            transform.position = Vector3.MoveTowards(transform.position, targetCirlePos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetCirlePos) < 0.05f)
            {
                transform.position = targetCirlePos; //针的位置设置为targetCirlePos
                transform.parent = circle;//针的父类设置为circle
                isFly = false;//设置为false，后上面的代码都不会被执行第二次，会跟着圆进行旋转，不会自身旋转

            }

        }

    }
    public void Startfly()
    {

        isFly = true;
        isReach = true;


    }
}
