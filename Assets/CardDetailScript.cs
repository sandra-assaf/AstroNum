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
        foreach (CardFlipScript unit in cards)
            unit.MouseDown += showDetail;
    }

    void showDetail(CardFlipScript card)
    {
        StartCoroutine(AnimateCard(card));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AnimateCard(CardFlipScript card)
    {
        float t = 0f;

        while (t < animationDuration)
        {
            t += Time.deltaTime;

            if (t > animationDuration)
            {
                t = animationDuration;
            }

            card.transform.position = Vector3.Lerp(card.transform.position, cardDetailPosition, t / animationDuration);
            yield return null;
        }

        detailView.gameObject.SetActive(true);
    }

}
