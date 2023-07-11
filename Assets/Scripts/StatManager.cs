using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class StatManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _statsNames;
    [SerializeField] private TextMeshProUGUI[] _statsNumbers;
    [SerializeField] private float[] _statsInt;
    
    private void Start()
    {
        UpdateStats(_statsInt);
    }

    public void UpdateStats(float[] stats)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            _statsInt[i] += stats[i];
            _statsNumbers[i].text = _statsInt[i].ToString();
        }
        UpdateColorStats(_statsInt);
    }

    public void UpdateColorStats(float[] stats)
    {
        for (int i = 0; i < _statsNumbers.Length; i++)
        {
            if (stats[i] < 0)
            {
                _statsNames[i].color = Color.red;
                _statsNumbers[i].color = Color.red;
            }
            else if (stats[i] > 0)
            {
                _statsNames[i].color = Color.green;
                _statsNumbers[i].color = Color.green;
            }
            else
            {
                _statsNames[i].color = Color.black;
                _statsNumbers[i].color = Color.black;
            }
        }
    }
}
