using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealerScript : MonoBehaviour
{
    public GameObject[] cards = new GameObject[5];
    public Vector3[] initialPositions = new Vector3[5];

    private int cardMovingIndex;

    public bool animatingDealing = false;
    public float animationDuration;
    public float dealingSpeed;

    void Start()
    {
        cardMovingIndex = 0;
    }

    void Update()
    {
        if(animatingDealing)
        {
            animatingDealing = false;
            StartCoroutine(AnimateCard());
        }
    }

    IEnumerator AnimateCard()
    {
        float t = 0f;

        while (t < animationDuration)
        {
            t += Time.deltaTime;

            cards[cardMovingIndex].transform.position = Vector3.Lerp(cards[cardMovingIndex].transform.position, initialPositions[cardMovingIndex], t / dealingSpeed);
            yield return null;
        }

        cardMovingIndex++;

        if (cardMovingIndex < 5)
        {
            StartCoroutine(AnimateCard());
        }
    }
}
