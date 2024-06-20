using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class fruitspawn : MonoBehaviour
{

    public GameObject[] fruits;
    public int TotalScore = 0;
    public static fruitspawn instance;
    //分数文本框
    public TMP_Text scoreText;
    //时间文本框
    public TMP_Text timeText;

    //剩余时间
    public float TimeRemin;
    //倒计时时间
    public float CountTime = 60f;
    //记录开始时间
    private float StartTime;
    //重新开始UI
    public GameObject RestartUI;
    //游戏是否进行
    public bool isPlaying;
    //重新开始UI的位置
    public Transform RestartUIPos;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowRestartUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!isPlaying)
        {
            return;
        }
        TimeRemin = CountTime - (Time.time - StartTime);
        timeText.text = FormateTime(TimeRemin);
        if (TimeRemin <= 0)
        {
            GameOver();
        }
    }
    private string FormateTime(float time)
    {
        string timeStr;
        int timeFormated = (int)time;
        timeStr = timeFormated > 10 ? "00:" + timeFormated.ToString() : "00:0" + timeFormated.ToString();
        return timeStr;
    }

    private void GameOver()
    {
        isPlaying = false;
        StopAllCoroutines();
        //延迟2秒显示重新开始UI
        Invoke("ShowRestartUI", 2f);

    }

    private void StartSpawn()
    {
        StartCoroutine(SpawnFruit());
    }

    IEnumerator SpawnFruit()
    {
        while (true)
        {
            //生成随机水果
            GameObject fruit = Instantiate(fruits[Random.Range(0, fruits.Length)]);
            //获取水果的刚体
            Rigidbody rb = fruit.GetComponent<Rigidbody>();
            //设置y轴的速度
            rb.velocity = new Vector3(0, 5f, 0);
            //设置角速度
            rb.angularVelocity = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
            //设置水果的位置
            Vector3 pos = transform.position + transform.right * Random.Range(-1.5f, 1.5f);
            fruit.transform.position = pos;
            //开启重力
            rb.useGravity = true;
            //执行间隔
            yield return new WaitForSeconds(0.5f);

        }
    }

    //加分
    public void AddScore(int score)
    {
        TotalScore += score;
        scoreText.text = TotalScore.ToString();
    }

    public void RestartGame()
    {
        Invoke("Init", 1.5f);
    }

    private void Init()
    {
        isPlaying = true;
        TotalScore = 0;
        scoreText.text = "0";
        timeText.text = "00:00";
        StartTime = Time.time;
        StartSpawn();
    }

    private void ShowRestartUI()
    {
        GameObject Restart = Instantiate(RestartUI);
        Restart.transform.position = RestartUIPos.position;
        Restart.transform.localScale = Vector3.zero;
        Restart.transform.DOScale(Vector3.one * 25, 0.4f);
    }
}
