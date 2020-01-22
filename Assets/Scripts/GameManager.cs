using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour

{
    public float life = 100;
    public float maxLife = 100;
    public float attack = 100;
    public float deffence = 100;
    public float attackExp = 1;
    public float deffenceExp = 1;
    public int score = 0;
    public int floor = 0;
    public int heal = 0;
    public GameObject obj;
    public Vector3 pos3Dnot = new Vector3(10000,10000,10000);       // 自分のいるフロアー外の敵を一時的に表示させなくするための物

    private GameObject goSM;
    private SoundMgr scSM;
    private GameObject goCM;
    private CreateMaze scCM;

    void Awake()
    {
//        goCM = GameObject.Find("CreateMazer");
//        scCM = goSM.GetComponent<CreateMaze>();
    }

    // Start is called before the first frame update
    void Start()
    {
        goSM = GameObject.Find("SoundManager");
        scSM = goSM.GetComponent<SoundMgr>();
        scSM.PlaySE(0);
        scSM.PlayBGM(true, true, 1);
        // 自動スリープを無効にする
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        // Cubeプレハブを元に、インスタンスを生成、

    }

    // Update is called once per frame
    void Update()
    {
    }
}
