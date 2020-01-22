using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Title : MonoBehaviour
{
    public GameObject goSM;
    public SoundMgr scSM;

    public string urlChkConnect = "https://www.edge-bp.com/test/test905/sql_chkconnect.php";
    public string urlGetCount = "https://www.edge-bp.com/test/test905/sql_getcount.php";
    public string urlInsert = "https://www.edge-bp.com/test/test905/sql_insert.php";
    public string urlGetHiscore = "https://www.edge-bp.com/test/test905/sql_gethiscore.php";

    public GameObject goConnect;
    public GameObject goName;
    public GameObject goScore;
    public GameObject goInputName;
    public GameObject goInputScore;

    string[] strConnect = { "接続中", "接続成功", "接続失敗" };
    private const string connectNG = "接続失敗";
    private string coStr;

    void Awake()
    {
        goConnect.GetComponent<Text>().text = strConnect[0];
        StartCoroutine(getCount(urlChkConnect));
    }

    // Start is called before the first frame update
    void Start()
    {
        goSM = GameObject.Find("SoundManager");
        scSM = goSM.GetComponent<SoundMgr>();
        scSM.PlayBGM(true, true, 0);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            OnClick();
        }
    }
    public void OnClick()
    {
        SceneManager.LoadScene(2);
    }


    IEnumerator getCount(string url)
    {
        IEnumerator coroutine;
        int num = 0;

        //
        // mariaDBに接続できるか。
        //
        coroutine = getWeb(urlChkConnect, num);
        yield return coroutine;
        // コルーチンからの戻り値をintに変換する
        num = (int)coroutine.Current;
        if (num == -1)
        {
            goConnect.GetComponent<Text>().text = strConnect[2];
        }
        else
        {
            if (num != 1)
            {
                goConnect.GetComponent<Text>().text = strConnect[2];
            }
            else
            {
                goConnect.GetComponent<Text>().text = strConnect[1];
            }
        }

        if (goConnect.GetComponent<Text>().text != strConnect[1]) yield break;

        //
        // 何人登録されているか
        //
        coroutine = getWeb(urlGetCount, num);
        yield return coroutine;
        // コルーチンからの戻り値をintに変換する
        num = (int)coroutine.Current;
        if (num == 0)
        {
            // 誰も登録されていない場合は、とりあえず一人登録する
            coroutine = setWeb(urlInsert, "kenji", 1);
            yield return coroutine;
        }

        //
        // hiscoreを得る
        //
        coroutine = getWeb(urlGetHiscore);
        yield return coroutine;
        // coStrから、一番最初に見つけたハイスコア(同一得点もあり得るので)をフィールドにセットする
        setFirsthiscore(coStr);
    }

    IEnumerator getWeb(string url, int num)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            num = -1;
            yield return num;
        }
        yield return int.Parse(www.downloadHandler.text);
    }


    IEnumerator getWeb(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            yield break;
        }
        // コルーチンの中は ref が使えないので、クラス内の coStr に結果を納めます。
        coStr = www.downloadHandler.text;
    }


    IEnumerator setWeb(string url, string name, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("score", score);
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        www.chunkedTransfer = false;
        yield return www.SendWebRequest();
        /*
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("error:"+www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    Debug.Log("MULTIPART: " + www.downloadHandler.text);
                }
        */
    }

    // coStrから、一番最初に見つけたハイスコア(同一得点もあり得るので)をフィールドにセットする
    void setFirsthiscore(string str)
    {
        int n = 0;
        string wStr = "";

        // 最初の名前を読み込み書き込む。
        while (str[n] != ',')
        {
            wStr += str[n++];
        }
        goName.GetComponent<Text>().text = wStr;
        n++;

        // 最初の数字を読み込む。
        wStr = "HISCORE:";
        while (str[n] != ',')
        {
            wStr += str[n++];
        }
        goScore.GetComponent<Text>().text = wStr;
    }

/*
    // ボタンが押されたらユーザーと得点をmariaDBに登録する
    public void OnClick()
    {
        if (goInputName.GetComponent<InputField>().text == "" || goInputScore.GetComponent<InputField>().text == "") return;
        // coStrから、一番最初に見つけたハイスコア(同一得点もあり得るので)をフィールドにセットする
        StartCoroutine("setInputData");

    }
*/
    IEnumerator setInputData()
    {
        IEnumerator coroutine;
        coroutine = setWeb(urlInsert, goInputName.GetComponent<InputField>().text, int.Parse(goInputScore.GetComponent<InputField>().text));
        yield return coroutine;
        coroutine = getWeb(urlGetHiscore);
        yield return coroutine;
        setFirsthiscore(coStr);
    }

}



