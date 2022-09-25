using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int itemID;
    public Image icon;
    public GameObject infoWindow;
    public float hp;
    public float str;
    public float hungry;
    public string type;

    public bool isEq;

    public GameObject alone;
    public GameObject son1;
    public GameObject son2;
    public GameObject son3;

    public void ItemInfoOn()
    {
        ItemDataReader.Instance.ItemInfo(itemID);
    }

    public void InfoOn()
    {
        infoWindow.SetActive(true);
    }

    public void InfoOff()
    {
        infoWindow.SetActive(false);
    }

    public void UseItem()
    {
        if (ItemDataReader.Instance.getItemValue[itemID] > 0)
        {
            //사용
            Debug.Log("아이템사용");
            if (type == "Used")
            {
                GM.Instance.player.hp += hp;
                GM.Instance.player.hungry += hungry;
                ItemDataReader.Instance.getItemValue[itemID]--;
            }
            else if (type == "ETC")
            {

            }

            else if (type == "Equipment")
            {
                if (isEq == false)
                {
                    GM.Instance.player.str += str;
                    isEq = true;
                    GM.Instance.eqitem.sprite = ItemDataReader.Instance.itemIcon[itemID];
                }
                else if(isEq == true)
                {
                    if(ItemDataReader.Instance.getItemValue[itemID] == ItemDataReader.Instance.getItemValue[itemID])
                    {
                        GM.Instance.player.str -= str;
                        isEq = false;
                        GM.Instance.eqitem.sprite = null;
                    }
                }
            }

        }
        else
        {
            //안사용
        }
    }
}
