using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitMenus : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] GameObject itemMenu;

    public void OnPointerClick(PointerEventData eventData)
    {
        CloseMenu();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    void CloseMenu()
    {
        itemMenu.SetActive(false);
    }
}
