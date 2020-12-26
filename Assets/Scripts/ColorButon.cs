using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorButon : MonoBehaviour, IPointerClickHandler
{
    public int textureIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        TableColor tableColor = GameObject.Find("Сustomization").GetComponent<TableColor>();
        tableColor.OpenMenu(false); //close choose menu
        tableColor.NewTexture(textureIndex);
    }
}
