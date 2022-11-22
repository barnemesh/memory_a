using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageReceiver : MonoBehaviour
{
    #region Inspector

    [Header("Images To Switch To")]
    [SerializeField]
    private Sprite correctStreet;

    [SerializeField]
    private Sprite wrongStreet;

    #endregion

    private Image _image;
    private static readonly int CurrentTime = Shader.PropertyToID("_CurrentTime");
    private static readonly int FadeOut = Shader.PropertyToID("_FadeOut");

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.material.SetInt(FadeOut, 1);
        _image.enabled = false;
        _image.sprite = correctStreet;
    }

    public void StreetImage(string action)
    {
        switch (action)
        {
            case "on":
                if (_image.material.GetInt(FadeOut) == 0)
                    break;
                _image.enabled = true;
                _image.material.SetInt(FadeOut, 0);
                _image.material.SetFloat(CurrentTime, Time.time);
                break;
            case "off":
                if (!_image.isActiveAndEnabled | _image.material.GetInt(FadeOut) == 1)
                    break;
                Debug.Log("here");
                _image.material.SetInt(FadeOut, 1);
                _image.material.SetFloat(CurrentTime, Time.time);
                break;
            default:
                _image.enabled = _image.enabled;
                break;
        }
    }

    public void ChangeLocation(string location)
    {
        _image.sprite = location switch
        {
            "Street" => correctStreet,
            "Wrong Street" => wrongStreet,
            _ => _image.sprite
        };
    }
}