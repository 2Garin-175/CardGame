using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TableColor : MonoBehaviour, IPointerClickHandler
{
    bool _active = false;
    int _index = 0; 

    public GameObject[] _buton =new GameObject[0];  //add on Unity

    public Sprite[] _sprites = new Sprite[0];  //add on Unity

    private void Start()
    {
        if(!PlayerPrefs.HasKey("TableKayIndex")) //if dont hawe save
        {
            PlayerPrefs.SetInt("TableKayIndex", _index);//set default index
        }
        else
        {
            _index = PlayerPrefs.GetInt("TableKayIndex");

            SetTexture(_index);
        }

        //IconAnimation

        foreach (GameObject g in _buton)
            g.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_active)
            OpenMenu(true);
        else
            OpenMenu(false);
    }

    public void NewTexture(int _textureIndex)   //change texture
    {
        PlayerPrefs.SetInt("TableKayIndex", _textureIndex);

        SetTexture(_textureIndex);
    }

    void SetTexture(int _textureIndex) //set tabel from array
    {
        GameObject.Find("table").GetComponent<SpriteRenderer>().sprite = _sprites[_textureIndex];
    }

    public void OpenMenu(bool _open)
    {
        foreach (GameObject g in _buton)
        {
            g.SetActive(_open);
        }

        if (_open)
        {
            GetComponent<Image>().color = new Color32(150, 150, 150, 255);
            _active = false;
        }
        else
        {
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            _active = true;
        }
    }
}
