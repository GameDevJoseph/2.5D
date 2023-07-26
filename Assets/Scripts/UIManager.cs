using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [SerializeField] TMP_Text _coinText;

    public static UIManager Instance
    {
        get 
        {
            if (_instance == null)
                Debug.LogError("UI Manager is null");

            return _instance; 
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        UpdateCoinDisplay(0);
    }

    public void UpdateCoinDisplay(int amount)
    {
        _coinText.text = "Coins: " + amount.ToString();
    }
}
