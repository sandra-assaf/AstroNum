using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealerScript : MonoBehaviour
{
    public CardFlipScript[] cards = new CardFlipScript[5];
    public Vector3[] initialPositions = new Vector3[5];

    private (Vector3, Vector3)[] interpolationPoints = new (Vector3, Vector3)[5];

    private int cardMovingIndex;

    private bool animatingDealing = false;
    public float animationDuration;
    public float dealingSpeed;
    public float interpolationConstant;

    void Start()
    {
        cardMovingIndex = 0;
        calculateBezierInterpolations();
        animatingDealing = true;
    }

    void Update()
    {
        if(animatingDealing)
        {
            animatingDealing = false;
            StartCoroutine(AnimateCard());
        }
    }

    void calculateBezierInterpolations()
    {
        for(int i = 0; i < 5; i++)
        {
            Vector3 p1 = new Vector3((interpolationConstant * ((i < 2) ? (-1) : 1)), 1, 0);
            Vector3 p2 = new Vector3(initialPositions[i].x + (interpolationConstant * ((i <= 2) ? (-1) : 1)), 1, 0);
            interpolationPoints[i] = (p1, p2);
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

            cards[cardMovingIndex].transform.position = cubeBezier3(cards[cardMovingIndex].transform.position, interpolationPoints[cardMovingIndex].Item1, interpolationPoints[cardMovingIndex].Item2, initialPositions[cardMovingIndex], t / animationDuration);
            yield return null;
        }

        cards[cardMovingIndex].startFloating();
        cardMovingIndex++;

        if (cardMovingIndex < 5)
        {
            StartCoroutine(AnimateCard());
        }
    }

    public static Vector3 cubeBezier3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return (((-p0 + 3 * (p1 - p2) + p3) * t + (3 * (p0 + p2) - 6 * p1)) * t + 3 * (p1 - p0)) * t + p0;
    }


}
