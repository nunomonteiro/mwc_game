using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Advantage : MonoBehaviour
{

    [SerializeField]
    private Text _title;

    [SerializeField]
    private Text _text;

    [SerializeField]
    private Image _image;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(string title, string text, string imgURL)
    {
        _title.text = title;
        _text.text = text;

        if (InternetAvailability.HasInternet())
            WWWImageFetcher.FillImageFromURL(_image, imgURL);
    }
}
