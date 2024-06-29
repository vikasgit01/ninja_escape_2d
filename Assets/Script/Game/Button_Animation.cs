using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Animation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (FindObjectOfType<Game_Manager>() != null)
        {
            Game_Manager.instance.canShoot = false;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (FindObjectOfType<Game_Manager>() != null)
        {
            Game_Manager.instance.canShoot = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        this.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
    }

}
