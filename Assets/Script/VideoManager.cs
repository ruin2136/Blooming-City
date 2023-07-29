using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using static System.Net.Mime.MediaTypeNames;

public class VideoManager : MonoBehaviour
{
    public GameObject player;
    public VideoPlayer vp;

    void Start()
    {
        vp.Prepare();

        VideoPrint();
    }

    void LoopEnd(VideoPlayer vp)
    {
        Debug.Log("비디오 끝");
        vp.loopPointReached -= LoopEnd;
        player.SetActive(false);

        //여기에 비디오 종료 시 작동할 코드 작성
    }

    public void VideoPrint()
    {
        player.SetActive(true);

        Debug.Log("비디오 시작");
        vp.Play();
        vp.isLooping = false;
        vp.loopPointReached += LoopEnd;
    }
}
