using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealerScript : MonoBehaviour
{
    public CardFlipScript[] cards = new CardFlipScript[5];
    public Vector3[] initialPositions = new Vector3[5];

    private (Vector3, Vector3)[] interpolationPoints = new (Vector3, Vector3)[5];

    private bool animatingDealing = false;
    public float animationDuration;
    public float dealingSpeed;
    public float interpolationConstant;

    void Start()
    {
        calculateBezierInterpolations();
        animatingDealing = true;
    }

    void Update()
    {
        if(animatingDealing)
        {
            animatingDealing = false;
            for (int i=0; i<5; i++)
            {
                StartCoroutine(AnimateCard(i));
            }
        }
    }

    void calculateBezierInterpolations()
    {
        for(int i = 0; i < 5; i++)
        {
            Vector3 p1 = new Vector3(((Mathf.Abs(initialPositions[i].x) / interpolationConstant) * ((i < 2) ? (-1) : 1)), 0f, 0);
            Vector3 p2 = new Vector3(initialPositions[i].x + ((Mathf.Abs(initialPositions[i].x))/interpolationConstant * ((i <= 2) ? (-1) : 1)), 0f, 0);
            interpolationPoints[i] = (p1, p2);
        }
    }

    IEnumerator AnimateCard(int cardIndex)
    {
        yield return new WaitForSeconds(0.5f);
        float t = 0f;

        while (t < animationDuration)
        {
            t += Time.deltaTime;

            if(t > animationDuration)
            {
                t = animationDuration;
            }

            cards[cardIndex].transform.position = cubeBezier3(cards[cardIndex].transform.position, interpolationPoints[cardIndex].Item1, interpolationPoints[cardIndex].Item2, initialPositions[cardIndex], t / animationDuration);
            yield return null;
        }

        cards[cardIndex].startFloating();
        cards[cardIndex].canHover = true;
    }

    public static Vector3 cubeBezier3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return (((-p0 + 3 * (p1 - p2) + p3) * t + (3 * (p0 + p2) - 6 * p1)) * t + 3 * (p1 - p0)) * t + p0;
    }


}
