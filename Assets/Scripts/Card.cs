using System.Collections;
using UnityEngine;


public class Card : MonoBehaviour
{
    public Sprite back;
    public Sprite front;

    public bool active = false;
    public int cost;
    public OPTIONS cardSuit;

    public enum OPTIONS
    {
        Spades = 0, //пики
        Hearts = 1, //чарви
        Diamonds = 2, //бубы
        Clubs = 3, //трефи
    }

    public void Upend() //if need turn over card
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer.sprite == back)
            spriteRenderer.sprite = front;
        else
            spriteRenderer.sprite = back;
    }

    public void MoveTo(Vector3 targetPosition)//move to target vector
    {
        _stepSize = (targetPosition - transform.position) / 60; //set pass, 60 steps
        StartCoroutine(Move());
    }

    private Vector3 _stepSize;//step size
    private IEnumerator Move()//60 frame rate
    {
        for (int i = 0; i < 60; i++)//step by step
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(_stepSize);
        }
    }

    private void OnMouseDown()
    {
        if (active)
        {
            GameObject.Find("Hand").GetComponent<Hand>().Click();//say hand about click on card
            MoveTo(new Vector3(0, 0, 0));
            StartCoroutine(DestroyCard());
        }
    }

    private IEnumerator DestroyCard()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        GameObject.Find("Score").GetComponent<Score>().AddScore(); //add score
    }
}
