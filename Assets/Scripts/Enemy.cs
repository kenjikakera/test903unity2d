using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float life;
    public float attack;
    public float deffence;
    public float speed;
    public float lifeAdd;
    public float attackExp;
    public float deffenceExp;
    public float searchArea;
    public int type;
    public int score;
    public int floor;
    public Vector3 pos3DReal;

    private int nWay;
    private bool isTrigger;
    private int nChace;

    private GameObject goGM;
    private GameManager scGM;
    private GameObject goPL;
    private CreateMaze scCM;


    // Start is called before the first frame update
    void Start()
    {
        nWay = Random.Range(0, 4);
        //cChange = 0;
        isTrigger = false;
        nChace = 1000;
        goGM = GameObject.Find("GameManager");
        scGM = goGM.GetComponent<GameManager>();
        scCM = goGM.GetComponent<CreateMaze>();
        goPL = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos3D;
        if (scGM.floor != floor)
        {
            transform.position = scGM.pos3Dnot;
            return;
        }
        else
        {
            // 出現させる
            pos3D = transform.position;
            if (pos3D == scGM.pos3Dnot )
            {
                transform.position = pos3DReal;
            }
        }

        nChace--;
        if (nChace < 0) nChace = 0;
        if (isTrigger == false && nChace==0 && Random.Range(0, 1000) > 950)
        {
            nWay = Random.Range(0, 4);
            
        }
        if(nChace == 0 ){
            if (Vector3.Distance(goPL.transform.position, transform.position) < searchArea)
            {
                nChace = 500;
            }
            else
            {
                nChace = 0;
            }
        }
        if ( isTrigger == false && nChace > 0 )
        {
            if (Mathf.Abs(goPL.transform.position.x - transform.position.x) < Mathf.Abs(goPL.transform.position.y - transform.position.y))
            {
                nWay = 2;
                if (goPL.transform.position.y < transform.position.y)
                {
                    nWay = 0;
                }
            } else
            {
                nWay = 1;
                if (goPL.transform.position.x < transform.position.x)
                {
                    nWay = 3;
                }
            }

            if (Random.Range(0, 1000) > 900)
            {
                nChace = 0;
            }
        }
        pos3D = transform.position;
        switch (nWay)
        {
            case 0:
                pos3D.y -= speed;
                break;
            case 1:
                pos3D.x += speed;
                break;
            case 2:
                pos3D.y += speed;
                break;
            case 3:
                pos3D.x -= speed;
                break;
        }
        transform.position = pos3D;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (isTrigger == false && coll.tag == "block")
        {
            nWay += 2;
            if (nWay > 3)
            {
                nWay -= 4;
            }
            isTrigger = true;
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (isTrigger == false && coll.tag == "block")
        {
            nWay += 2;
            if (nWay > 3)
            {
                nWay -= 4;
            }
            isTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        isTrigger = false;
    }
}
