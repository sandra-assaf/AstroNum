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

            if(t > animationDuration)
            {
                t = animationDuration;
            }

            //cards[cardMovingIndex].transform.position = Vector3.Lerp(cards[cardMovingIndex].transform.position, initialPositions[cardMovingIndex], t / dealingSpeed);
            cards[cardMovingIndex].transform.position = cubeBezier3(cards[cardMovingIndex].transform.position, new Vector3(-3,1,0), new Vector3(-10f, 1, 0), initialPositions[cardMovingIndex], t / animationDuration);
            yield return null;
        }

        cardMovingIndex++;

        /*if (cardMovingIndex < 5)
        {
            StartCoroutine(AnimateCard());
        }*/
    }

    public static Vector3 cubeBezier3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return (((-p0 + 3 * (p1 - p2) + p3) * t + (3 * (p0 + p2) - 6 * p1)) * t + 3 * (p1 - p0)) * t + p0;
    }


}
