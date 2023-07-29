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
        Debug.Log("���� ��");
        vp.loopPointReached -= LoopEnd;
        player.SetActive(false);

        //���⿡ ���� ���� �� �۵��� �ڵ� �ۼ�
    }

    public void VideoPrint()
    {
        player.SetActive(true);

        Debug.Log("���� ����");
        vp.Play();
        vp.isLooping = false;
        vp.loopPointReached += LoopEnd;
    }
}
