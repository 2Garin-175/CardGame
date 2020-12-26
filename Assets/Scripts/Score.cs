using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    int _score =-1;
    public void AddScore()
    {
        _score++;
        if(_score > 0)
            GetComponent<Text>().text = Convert.ToString(_score) + "/10";
    }
}
