using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardsScript : MonoBehaviour
{
    private List<int> pickedCards;

    public int numberOfCards;
    public int numberOfPickedCards;


    void Start()
    {
        pickedCards = new List<int>();
        for(int i = 0; i < numberOfPickedCards; i++)
        {
            pickedCards.Add(UniqueRandomInt(0, numberOfCards));
        }

        //DEBUG
        for (int i = 0; i < numberOfPickedCards; i++)
        {
            Debug.Log(pickedCards[i]);
        }
        Debug.Log("-------");

    }

    void Update()
    {

    }
    public int UniqueRandomInt(int min, int max)
    {
        int val = Random.Range(min, max);
        while (pickedCards.Contains(val))
        {
            val = Random.Range(min, max);
        }
        return val;
    }
}
