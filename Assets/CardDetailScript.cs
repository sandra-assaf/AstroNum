using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CardDetailScript : MonoBehaviour
{
    public CardFlipScript[] cards = new CardFlipScript[5];
    public Vector3 cardDetailPosition;
    public Canvas detailView;

    private float animationDuration = 0.5f;
    private Vector3 initialPosition;


    // Start is called before the first frame update
    void Start()
    {
        foreach (CardFlipScript unit in cards)
        {
            unit.MouseDown += showDetail;
            unit.MouseDownAgain += retractDetail;
        }
            
    }

    void retractDetail(CardFlipScript card)
    {
        detailView.gameObject.SetActive(false);
        StartCoroutine(AnimateCard(card, false));
    }

    void showDetail(CardFlipScript card)
    {
        initialPosition = card.transform.position;
        StartCoroutine(AnimateCard(card, true));
    }

    IEnumerator AnimateCard(CardFlipScript card, bool forward)
    {
        float t = 0f;

        while (t < animationDuration)
        {
            t += Time.deltaTime;

            if (t > animationDuration)
            {
                t = animationDuration;
            }

            card.transform.position = Vector3.Lerp(card.transform.position, forward ? cardDetailPosition : initialPosition, t / animationDuration);
            yield return null;
        }

        detailView.gameObject.SetActive(forward);

        foreach (CardFlipScript cardObject in cards)
            if (cardObject.order != card.order)
            {
                cardObject.canHover = !forward;
            }
    }

}
