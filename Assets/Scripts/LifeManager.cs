using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LifeManager : MonoBehaviour
{

    public Image cooldown;

    // Update is called once per frame

    // 体力のgo,GameMasterのGOを取得

    private GameObject goGM;
    private GameManager scGM;

    private void Awake()
    {
        goGM = GameObject.Find("GameManager");
        scGM = goGM.GetComponent<GameManager>();
    }

    void Update()
    {
        cooldown.fillAmount = scGM.life / scGM.maxLife;
    }
}
