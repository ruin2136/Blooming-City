using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2BtnImage : MonoBehaviour
{
    Image image;
    public Sprite repairImage;
    void Start()
    {
        if (GameManager.instance.GetNumOfClearStage() == 1)
            gameObject.GetComponent<Button>().enabled = true;
        else
            gameObject.GetComponent<Button>().enabled = false;

        image = GetComponent<Image>();

        if (GameManager.instance.isClearStage[1])
        {
            image.sprite = repairImage;
            gameObject.GetComponent<Button>().enabled = false;
        }
    }
}
