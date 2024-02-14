using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{
    public Image tpImage;
    public Image tp2Image;
    public Image input1Image;
    public Image input2Image;
    public Sprite emptyTp;
    public Sprite tp1;
    public Sprite tp2;
    public Sprite tpInactive;
    public Sprite input1Active;
    public Sprite input2Active;
    public Sprite input1Inactive;
    public Sprite input2Inactive;

    // Start is called before the first frame update
    public void fillTp1()
    {
        if(NetworkManager.Singleton.LocalClientId == 0)
        {
            tpImage.sprite = tp1;
            input1Image.sprite = input1Active;
        }
        else
        {
            tpImage.sprite = tp2;
            input1Image.sprite = input1Active;
        }
        
    }
    public void fillTp2()
    {
        if(NetworkManager.Singleton.LocalClientId == 0)
        {
            tp2Image.sprite = tp2;
            input2Image.sprite = input2Active;
        }
        else
        {
            tp2Image.sprite = tp1;
            input2Image.sprite = input2Active;
        }
    }

    public void emptyTp1()
    {
        tpImage.sprite = emptyTp;
        input1Image.sprite = input1Inactive;
    }
    public void emptyTp2()
    {
        tp2Image.sprite = emptyTp;
        input2Image.sprite = input2Inactive;
    }

    public void inactiveTp1()
    {
        tpImage.sprite = tpInactive;
        input1Image.sprite = input1Inactive;
    }

    public void inactiveTp2()
    {
        tp2Image.sprite = tpInactive;
        input2Image.sprite = input2Inactive;
    }
}
