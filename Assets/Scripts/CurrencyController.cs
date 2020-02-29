using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    public static CurrencyController Instance = null;

    public static int Currency = 0;

    private void Awake()
    {
        Instance = this;
    }

    public static void AddCurrency(int currencyToAdd)
    {
        Currency += currencyToAdd;
        Debug.Log("Currency: " + Currency.ToString());
    }

    public static void SubtractCurrency(int currencyToSubtract)
    {
        if (Currency <= 0)
        {
            Debug.Log("Not enough currency");
        }
        else
        {
            Currency -= currencyToSubtract;
            Debug.Log("Currency: " + Currency.ToString());
        }
    }

    public static void ResetCurrency()
    {
        Currency = 0;
        Debug.Log("Currency: " + Currency.ToString());
    }
}
