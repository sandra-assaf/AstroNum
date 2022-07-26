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


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            cards[i].detailEvent.AddListener(moveCardToPosition);
        }
    }

    void moveCardToPosition()
    {
        for(int i = 0; i < 5; i++)
        {
            if(cards[i].cardState == CardFlipScript.CardFlipState.FlippedDetail)
            {
                StartCoroutine(AnimateCard(i));
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AnimateCard(int cardIndex)
    {
        float t = 0f;

        while (t < animationDuration)
        {
            t += Time.deltaTime;

            if (t > animationDuration)
            {
                t = animationDuration;
            }

            cards[cardIndex].transform.position = Vector3.Lerp(cards[cardIndex].transform.position, cardDetailPosition, t / animationDuration);
            yield return null;
        }

        detailView.gameObject.SetActive(true);
    }

}
