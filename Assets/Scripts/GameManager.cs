using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Avrahamy;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;


    [SerializeField]
    private TextMeshProUGUI mainText;

    [SerializeField]
    private GameObject endButton;

    [SerializeField]
    [Multiline]
    private string endText = "Somebody did come at the end.";

    [SerializeField]
    [HideInInspector]
    private string waitText = "Here they are.";  // TODO: remove this option, only 1 finish

    [SerializeField]
    private PassiveTimer waitTimer;


    public static string MainText
    {
        get => _instance.mainText.text;
        set => _instance.mainText.text = value;
    }

    public static int OptionCount { get; set; }


    public static void ActivateEnd()
    {
        _instance.waitTimer.Clear();
        _instance.endButton.SetActive(true);
        MainText = _instance.endText;
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(_instance.endButton);
    }

    public static void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    private void Awake()
    {
        if (_instance is null)
        {
            _instance = this; // TODO: Timer to wait 15 minutes and end by itself.
            endButton.SetActive(false);
            waitTimer.Start();
            return;
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        if (waitTimer.IsActive | !waitTimer.IsSet)
        {
            return;
        }

        waitTimer.Clear();
        var options = FindObjectsOfType<OptionController>();
        foreach (var option in options)
        {
            option.gameObject.SetActive(false);
        }

        endText = waitText;
        ActivateEnd();
    }
}