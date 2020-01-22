using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundMgr : MonoBehaviour
{
    public bool isSeMute;
    public float seVolume = 1f;
    public bool isBgmMute;
    public float bgmVolume = 1f;
    public AudioSource[] audioSources;
    public AudioClip[] seClip;
    public float fadeSpeed = 3;

    private int nowBGM = -1;
    private bool fadeFlag = false;

    [SerializeField]
    private const float asMinDistance = 10.0f;
    private const float asMaxDistance = 30.0f;

    void Awake()
    {
        isSeMute = false;
        isBgmMute = false;

        // シーン遷移でも壊れないGameObjectにする
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(1);
    }
 

    //
    //  BGM(AudioSource)のクロスフェーダルーチン
    //
    void FixedUpdate()
    {
        for (int n = 0; n < audioSources.Length; n++)
        {
            if (isBgmMute)
            {
                if (n == nowBGM)
                {
                    audioSources[n].volume = 0;
                }
                else
                {
                    audioSources[n].Stop();
                }
            } else
            {
                if (n == nowBGM)
                {
                    audioSources[n].volume += fadeSpeed;
                }
                else
                {
                    float vol = audioSources[n].volume;
                    if(vol<=fadeSpeed)
                    {
                        audioSources[n].Stop();
                    } else
                    {
                        audioSources[n].volume -= fadeSpeed;
                    }
                }
            }

        }
   }


    // 効果音を鳴らす
    public void PlaySE(int num)
    {
        if (isSeMute) return;

        GameObject soundObj = new GameObject("SE");

        AudioSource _audioSource = soundObj.AddComponent<AudioSource>();
        _audioSource.clip = seClip[num];
        _audioSource.minDistance = asMinDistance;
        _audioSource.maxDistance = asMaxDistance;
        _audioSource.volume = seVolume;
        _audioSource.Play();

        Destroy(soundObj, seClip[num].length);
    }

    /*
        // 効果音を鳴らす。2Dポジション付き。
        public void PlaySE(Vector2 pos, int num)
        {
            if (isSeMute) return;

            GameObject soundObj = new GameObject("SE");
            soundObj.transform.position = pos;

            AudioSource _audioSource = soundObj.AddComponent<AudioSource>();
            _audioSource.clip = seClip[num];
            _audioSource.minDistance = 10.0f;
            _audioSource.maxDistance = 30.0f;
            _audioSource.volume = seVolume;
            _audioSource.Play();

            Destroy(soundObj, seClip[num].length);
        }


        // 効果音を鳴らす。3Dポジション付き。
        public void PlaySE(Vector3 pos, int num)
        {
            if (isSeMute) return;

            GameObject soundObj = new GameObject("SE");
            soundObj.transform.position = pos;

            AudioSource _audioSource = soundObj.AddComponent<AudioSource>();
            _audioSource.clip = seClip[num];
            _audioSource.minDistance = 10.0f;
            _audioSource.maxDistance = 30.0f;
            _audioSource.volume = seVolume;
            _audioSource.Play();

            Destroy(soundObj, seClip[num].length);
        }
    */


    // BGM(AudioSource)をならす。
    public void PlayBGM(bool bFade, bool bLoop, int num)
    {
        if (num == nowBGM) return;
        if (bFade == false && nowBGM == -1) audioSources[nowBGM].Stop();

        nowBGM = num;
        fadeFlag = bFade;

        audioSources[nowBGM].loop = bLoop;

        audioSources[num].volume = 0;
        if (isBgmMute)
        {
            audioSources[num].volume = 0;
        }
        else
        {
            if(!bFade) audioSources[num].volume = 1;
            audioSources[num].Play();
        }
    }

    // BGMを止める。
    public void StopBgm(ulong num)
    {
        audioSources[num].Stop();
    }

}
