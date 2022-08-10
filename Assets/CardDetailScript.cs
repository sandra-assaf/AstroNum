using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDetailScript : MonoBehaviour
{
    public CardFlipScript[] cards = new CardFlipScript[5];
    public Vector3 cardDetailPosition;
    public Canvas detailView;
    public SpriteRenderer bgSquare;
    public SpriteRenderer bgChenar;
    public TMP_Text cardDescription;

    private float animationDuration = 0.5f;
    private float textAnimationDuration = 2f;
    private Vector3 initialPosition;
    private float bgSquareAlpha = 0.92f;

    void Start()
    {
        foreach (CardFlipScript unit in cards)
        {
            unit.MouseDown += showDetail;
            unit.MouseDownAgain += retractDetail;
        }
        cardDescription.alpha = 0;     
    }

    void retractDetail(CardFlipScript card)
    {
        bgSquare.color = new Color(bgSquare.color.r,
                                                    bgSquare.color.g,
                                                    bgSquare.color.b,
                                                    0);
        bgChenar.color = new Color(bgChenar.color.r,
                                                    bgChenar.color.g,
                                                    bgChenar.color.b,
                                                    0);
        cardDescription.alpha = 0;
        StartCoroutine(AnimateCard(card, false));
        activateOtherCards(card.order, true);
    }

    void showDetail(CardFlipScript card)
    {
        initialPosition = card.transform.position;
        activateOtherCards(card.order, false);
        foreach (CardFlipScript cardObject in cards)
            if (cardObject.order != card.order)
            {
                cardObject.forceZoomOut();
            }
        StartCoroutine(AnimateCard(card, true));
    }

    public void activateOtherCards(int index, bool activate)
    {
        foreach (CardFlipScript cardObject in cards)
            if (cardObject.order != index)
            {
                cardObject.canHover = activate;
                cardObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, activate? 1f : 0.4f);
            }
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

            if(forward)
            {
                float alphaValue = Mathf.MoveTowards(0, bgSquareAlpha, t / animationDuration);

                bgSquare.color = new Color(bgSquare.color.r,
                                                        bgSquare.color.g,
                                                        bgSquare.color.b,
                                                        alphaValue);
                cardDescription.alpha = alphaValue + 1 - bgSquareAlpha;
                bgChenar.color = new Color(bgChenar.color.r,
                                                        bgChenar.color.g,
                                                        bgChenar.color.b,
                                                        cardDescription.alpha);
                cardDescription.alpha = alphaValue + 1 - bgSquareAlpha;
            }

            card.transform.position = Vector3.Lerp(card.transform.position, forward ? cardDetailPosition : initialPosition, t / animationDuration);
            yield return null;
        }

    }
}
