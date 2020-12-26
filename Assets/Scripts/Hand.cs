using System;
using System.Collections;
using UnityEngine;

public class Hand : MonoBehaviour
{
    GameObject[] _cardInHand;
    int[] _tableInfo = new int[4];      //Cardsuit and card count

    void Start()
    {
        StartCoroutine(Main());
    }

    private IEnumerator Main()      //find and take start card
    {
        yield return new WaitForSeconds(1);

        FindAllCard();

        MoveCard(true);

        yield return new WaitForSeconds(3);

        StartRandome();
    }

    void FindAllCard()
    {
        _cardInHand = GameObject.FindGameObjectsWithTag("Card");
        _cardInHand = Sort(_cardInHand);         //sorting cards
    }

    void StartRandome()         //make one cardsuit playable
    {
        int _testCardsuit = 0;
        foreach (int i in _tableInfo)       //check free Cardsuit;
        {
            if(i != 0)
            {
                _testCardsuit++;
            }
        }

        if (_testCardsuit != 0)
        {
            int _random = UnityEngine.Random.Range(0, 4);       //take random Cardsuit

            while (true)         //start new random if no definite Cardsuit
            {
                if (_tableInfo[_random] != 0)
                {
                    ActivateCardsuit(_random);
                    break;
                }
            }
        }
    }

    public void Click()
    {
        StartCoroutine(C());
    }
    private IEnumerator C()      //called after click on card
    {
        SetToDefaultAllCard();

        yield return new WaitForSeconds(4);

        FindAllCard();
        MoveCard(false);

        yield return new WaitForSeconds(3);

        StartRandome();
    }


    GameObject[] Sort(GameObject[] cards)        //sorting cards
    {
        _tableInfo = new int[4] { 0, 0, 0, 0 };     //clear table info

        ArrayClass<GameObject> bufer = new ArrayClass<GameObject>();

        for (int i = 0; i < 4; i++)               //what sort
            foreach (GameObject g in cards)             //serch card whis i
            {
                if (Convert.ToInt32(g.GetComponent<Card>().cardSuit) == i)      //take cardsuit index
                {
                    bufer.AddComponent(g);      //add obj to array
                    _tableInfo[i]++;
                }
            }

        return bufer.ReturnArr();
    }

    void MoveCard(bool Upend)
    {
        float _bias = 6.5f / _cardInHand.Length;         //6.5f - constant bar size //slot size for one card
        float _slot = -_cardInHand.Length / 2 * _bias;      //start from left, serch first position 

        foreach (GameObject g in _cardInHand)
        {
            Card card = g.GetComponent<Card>();
            
            if (Upend)
                card.Upend();        //turn over card

            card.MoveTo(new Vector3(_slot, -6.3f, -_slot));     //(slot position , constant , add depth from position)
            _slot += _bias;      //next slot
        }
    }

    void ActivateCardsuit(int _cardsuit)         //choose cardsuit
    {
        float _bias = 6.5f / (_cardInHand.Length + _tableInfo[_cardsuit]);       //6.5f - constant bar size //slot size for one card //add empty slots
        float _slot = -(_cardInHand.Length + _tableInfo[_cardsuit]) / 2 * _bias;         //start from left, serch first position //with ampty slots

        foreach (GameObject g in _cardInHand)
        {
            Card card = g.GetComponent<Card>();
            card.MoveTo(new Vector3(_slot, -6.3f, -_slot));         //(slot position , constant , add depth from position)

            if (Convert.ToInt32(g.GetComponent<Card>().cardSuit) == _cardsuit)      //add more space for need card //change color
            {
                card.active = true;
                g.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);   //on
                _slot += _bias;         //more space
            }
            else
                g.GetComponent<SpriteRenderer>().color = new Color32(160, 160, 160, 255);    //off

            _slot += _bias;         //next slot
        }
    }

    void SetToDefaultAllCard()      //set default setting all card
    {
        foreach (GameObject g in _cardInHand)
        {
            Card _card = g.GetComponent<Card>();

            _card.active = false;

            g.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255); //set active color
        }
    }
}

public class ArrayClass<Type>       //array can be resize
{
    Type[] _arr = new Type[0];

    public Type[] ReturnArr()
    {
        return _arr;
    }

    public void AddComponent(Type _newObj)      //Resize array for new componnent
    {
        Array.Resize(ref _arr, _arr.Length + 1);
        _arr[_arr.Length - 1] = _newObj;
    }
}
