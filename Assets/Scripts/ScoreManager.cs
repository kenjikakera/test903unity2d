using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{

    private GameObject goGM;
    private GameManager scGM;
    private GameObject goSC;

    private void Awake()
    {
        goGM = GameObject.Find("GameManager");
        scGM = goGM.GetComponent<GameManager>();
        goSC = GameObject.Find("Score");
    }

    void Update()
    {
        goSC.GetComponent<Text>().text = "SCORE:" + scGM.score.ToString();
    }

}
