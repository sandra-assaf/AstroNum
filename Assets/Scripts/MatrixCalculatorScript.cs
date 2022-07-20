using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatrixCalculatorScript : MonoBehaviour
{
    private int[] matrixVector;
    private bool updateUI;

    public TMP_InputField dayInput;
    public TMP_InputField monthInput;
    public TMP_InputField yearInput;

    public Button calculateButton;

    public TMP_Text[] matrixBoxes;

    void Start()
    {
        this.calculateButton.onClick.AddListener(onButtonClick);
        this.updateUI = false;
    }

    void Update()
    {
        if (updateUI)
        {
            for (int i = 0; i < matrixVector.Length; i++)
            {
                matrixBoxes[i].text = "";
                for (int j = 0; j < matrixVector[i]; j++)
                {
                    matrixBoxes[i].text += i + 1;
                }

            }
            updateUI = false;
        }
    }

    private void onButtonClick()
    {
        this.matrixVector = new int[9];

        int day = System.Convert.ToInt32(dayInput.text);
        int month = System.Convert.ToInt32(monthInput.text);
        int year = System.Convert.ToInt32(yearInput.text);

        getDigits(day);
        getDigits(month);
        getDigits(year);

        int firstNo = sumDigits(day) + sumDigits(month) + sumDigits(year);
        int secondNo = sumDigits(firstNo);
        int thirdNo = firstNo - 2 * getFirstDigit(day);
        int fourthNo = sumDigits(thirdNo);

        getDigits(firstNo);
        getDigits(secondNo);
        getDigits(thirdNo);
        getDigits(fourthNo);

        updateUI = true;
    }

    int sumDigits(int number)
    {
        int sum = 0;
        int digit = 0;
        while (number != 0)
        {
            digit = number % 10;
            number /= 10;
            sum += digit;

        }
        return sum;
    }

    int getFirstDigit(int number)
    {
        while (number / 10 != 0)
        {
            number /= 10;
        }

        return number % 10;
    }

    void getDigits(int number)
    {
        int digit = 0;
        while (number != 0)
        {
            digit = number % 10;
            number /= 10;
            if (digit != 0)
            {
                this.matrixVector[digit - 1] += 1;
            }

        }
    }
}
