using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    private GameObject goSM;
    private SoundMgr scSM;
    private GameObject goPL;
    private Player scPL;

    void Awake()
    {
        goPL = GameObject.Find("Player");
        scPL = goPL.GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            OnClick(0);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            OnClick(1);
        }
    }
    public void OnClick(int num)
    {
        switch( num)
        {
            case 0:
                scPL.actionButton();
                break;
            case 1:
                scPL.useButton();
                break;
        }
    }
}
