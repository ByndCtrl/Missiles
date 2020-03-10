using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : Singleton<CurrencyController>
{
    public int Currency = 0;

    public void AddCurrency(int currencyToAdd)
    {
        Currency += currencyToAdd;
        Debug.Log("Currency: " + Currency.ToString());
    }

    public void SubtractCurrency(int currencyToSubtract)
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

    public void ResetCurrency()
    {
        Currency = 0;
        Debug.Log("Currency: " + Currency.ToString());
    }
}
