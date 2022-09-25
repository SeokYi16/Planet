using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGetItem : MonoBehaviour
{
    public Transform player;

    public Vector2 endPos;

    GameObject target = null;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, LayerMask.GetMask("Furniture"));
            if (hit)
            {
                AudioManager.instance.PlaySound("ItemFind");

                if (hit.collider.gameObject.tag == "Sofa")
                {
                    target = hit.collider.gameObject;
                    target.GetComponent<BoxCollider2D>().enabled = false;
                    Destroy(target);
                    GM.Instance.RandomSofaItem();
                }
                else if (hit.collider.gameObject.tag == "Drawer")
                {
                    target = hit.collider.gameObject;
                    target.GetComponent<BoxCollider2D>().enabled = false;
                    Destroy(target);
                    GM.Instance.RandomDrawerItem();
                }
                else if (hit.collider.gameObject.tag == "Box")
                {
                    target = hit.collider.gameObject;
                    target.GetComponent<BoxCollider2D>().enabled = false;
                    Destroy(target);
                    GM.Instance.RandomBoxItem();
                }
                else if (hit.collider.gameObject.tag == "Refrigerator")
                {
                    target = hit.collider.gameObject;
                    target.GetComponent<BoxCollider2D>().enabled = false;
                    Destroy(target);
                    GM.Instance.RandomRefrigeratorItem();
                }
                else if (hit.collider.gameObject.tag == "MedicalBox")
                {
                    target = hit.collider.gameObject;
                    target.GetComponent<BoxCollider2D>().enabled = false;
                    Destroy(target);
                    GM.Instance.RandomMedicalBoxItem();
                }
                else if (hit.collider.gameObject.tag == "Key")
                {
                    target = hit.collider.gameObject;
                    target.GetComponent<BoxCollider2D>().enabled = false;
                    Destroy(target);
                    GM.Instance.KeyItem();
                }
                else
                {
                    Debug.Log("땅터치");
                }
            }
        }
    }
}
