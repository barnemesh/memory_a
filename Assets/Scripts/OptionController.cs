using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private TextMeshProUGUI symbol;

    [SerializeField]
    [Multiline]
    private string onSelectString;

    [SerializeField]
    private bool removeAfterClick = true;

    public string Symbol
    {
        get => symbol.text;
        set => symbol.text = value;
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        symbol = transform.Find("Symbol").GetComponent<TextMeshProUGUI>();
        GameManager.OptionCount++;
    }

    public void OnClick()
    {
        Debug.Log(gameObject.name, this);
        GameManager.MainText = onSelectString;

        if (removeAfterClick)
        {
            GameManager.OptionCount--;
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
            gameObject.SetActive(false);
            return;
        }

        switch (GameManager.OptionCount)
        {
            case > 1:
                return;
            case 1:
                GameManager.OptionCount--;
                return;
        }

        GameManager.ActivateEnd();
        gameObject.SetActive(false);
    }
}