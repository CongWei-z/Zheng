using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //导入ui模块
using UnityEngine.SceneManagement;//导入场景模块

public class Gamemanager : MonoBehaviour
{


    private Transform start; //获得Transform组件
    private Transform spawn;//获得Transform组件
    private Pin currentPin; //获得Pin组件，当前准备发射的针
    private bool isgameover = false; //布尔类型，是否游戏结束
    private Camera maincamera;//获得Camera组件
    private int score = 0;//显示分数


    public GameObject pinPrefab;//获得组件定义生成的对象：针，自定义名称为pinPrefab，引擎中选中Pin
    public Text scoreText;//获得组件，拖拽赋值text
    public float AnimSpeed = 3;//动画速度
   


    // Start is called before the first frame update
    void Start()//开始运行的事件
    {
        start = GameObject.Find("Start").transform; //找到引擎中start的组件
        spawn = GameObject.Find("Spawn").transform;//找到引擎中spawn的组件
        SpawnPin();  //生成针

        maincamera = Camera.main;//引擎摄像头组件
    }
    private void Update()
    {
        //当isgameover为TRUE时，结束整个Update函数
        if (isgameover) return;
        if (Input.GetMouseButtonDown(0))//每帧判定玩家是否点击鼠标
        {

            score++; //自增加
            scoreText.text = score.ToString();//赋值给游戏中的text

            currentPin.Startfly();//开始飞行
            SpawnPin();//重新生成针
        }
    }
    void SpawnPin()
    {
        //实例化对象是针，生成位置在spawn处 ，旋转角度是针的旋转方向和角度    
        //Pin的引用，可以调取Pin里的方法，GetComponent<Pin>获取pin组件
        currentPin = GameObject.Instantiate(pinPrefab,spawn.position,pinPrefab.transform.rotation).GetComponent<Pin>();   
    }
   
    public void Gameover ()
    {
        //程序先执行 isgameover = true，第二次执行到的时候得出isgameover是true，直接结束这个函数。
        if (isgameover) return;
        //查找到引擎中的Circle，获取Rotateself组件，设置为false，取消圆的旋转，enabled = false就是暂停脚本的函数
        GameObject.Find("Circle").GetComponent<Rotateself>().enabled = false;
        StartCoroutine(GameOverAnimation());//StartCoroutine协程，调用IEnumerator定义的方法
        isgameover = true;
    
    }

    IEnumerator GameOverAnimation()
    {
        while (true)//循环
        {
            //lerp差值，逐渐趋近于目标颜色
            maincamera.backgroundColor = Color.Lerp(maincamera.backgroundColor, Color.red, AnimSpeed * Time.deltaTime);
            //lerp差值，逐渐趋近于目标大小
            maincamera.orthographicSize = Mathf.Lerp(maincamera.orthographicSize, 4, AnimSpeed * Time.deltaTime);
            //判断是否趋近与4，然后结束循环
            if (Mathf.Abs(maincamera.orthographicSize - 4) < 0.01f)
            {

                break;
            }
            yield return 0;//等待一帧
        }
        yield return new WaitForSeconds(1); //等待新的方法持续1s
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//场景跳转，重新开始关卡
    
    }



}
