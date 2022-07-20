using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckShuffleScript : MonoBehaviour
{
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;

    public Button shuffleButton;

    private bool isAnimating = false;
    private bool forward;
    
    public float speedCard1;
    public float speedCard2;
    public float speedCard3;

    private Vector3 initialPositionCard1;
    private Vector3 initialPositionCard2;
    private Vector3 initialPositionCard3;

    public Vector3 card1RefPosition;
    public Vector3 card2RefPosition;
    public Vector3 card3RefPosition;

    private Vector3 card1TargetPosition;
    private Vector3 card2TargetPosition;
    private Vector3 card3TargetPosition;

    public float animationDuration;

    void Start()
    {
        initialPositionCard1 = new Vector3(0,0,-1);
        initialPositionCard2 = new Vector3(0, 0, 0);
        initialPositionCard3 = new Vector3(0, 0, 0);
        forward = true;
        shuffleButton.onClick.AddListener(startShuffle);
    }

    void Update()
    {
        
    }

    void startShuffle()
    {
        this.isAnimating = !this.isAnimating;
        if (this.isAnimating)
        {
            StartCoroutine(AnimateCard());
        } 
    }

    IEnumerator AnimateCard()
    {
        float t = 0f;
        card1TargetPosition = variablePosition(card1RefPosition);
        card2TargetPosition = variablePosition(card2RefPosition);
        card3TargetPosition = variablePosition(card3RefPosition);

        while (t < animationDuration)
        {
            t += Time.deltaTime;

            card1.transform.position = Vector3.Lerp(card1.transform.position, forward? card1TargetPosition : initialPositionCard1, t/speedCard1);
            card2.transform.position = Vector3.Lerp(card2.transform.position, forward ? card2TargetPosition : initialPositionCard2, t/speedCard2);
            card3.transform.position = Vector3.Lerp(card3.transform.position, forward ? card3TargetPosition : initialPositionCard3, t / speedCard3);

            yield return null;
        }
        forward = !forward;
        if(this.isAnimating)
        {
            StartCoroutine(AnimateCard());
        } else if (!forward)
        {
            StartCoroutine(AnimateCard());
        }
        Debug.Log(forward);
    }

    Vector3 variablePosition( Vector3 initialPosition)
    {
        float randomDifference = Random.Range(-2.0f, 2.0f);
        return new Vector3(initialPosition.x + randomDifference, initialPosition.y, initialPosition.z);
    }


}
