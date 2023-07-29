using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Player;

public class PlayerUI : MonoBehaviour
{
    public Player ply;

    public Sprite[] playerTypes;

    // Update is called once per frame
    void Update()
    {
        int i = (int)ply.actionType;

        GetComponent<Image>().sprite = playerTypes[i];
    }
}
