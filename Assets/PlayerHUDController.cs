using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerHUDController : MonoBehaviour
{
    public Image tpImage;
    public Image tp2Image;
    public Image input1Image;
    public Image input2Image;
    public GameObject textParent;
    public GameObject textParent1;
    public GameObject textParent2;
    public GameObject textParent3;
    public Sprite emptyTp;
    public Sprite tp1;
    public Sprite tp2;
    public Sprite tpInactive;
    public Sprite input1Active;
    public Sprite input2Active;
    public Sprite input1Inactive;
    public Sprite input2Inactive;



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Controls1"))
        {
            textParent.SetActive(false);
            textParent1.SetActive(true);

        }
        if (other.gameObject.CompareTag("Controls2"))
        {
            textParent1.SetActive(false);
            textParent2.SetActive(true);

        }
        if (other.gameObject.CompareTag("Controls3"))
        {
            textParent2.SetActive(false);
            textParent3.SetActive(true);

        }
    }


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
