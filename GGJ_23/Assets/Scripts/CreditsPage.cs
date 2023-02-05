using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPage : MonoBehaviour
{
    public GameObject CreditsImage;

    public void ShowCredits()
    {
        CreditsImage.SetActive(true);
    }

    public void ShowCredits2()
    {
        CreditsImage.SetActive(false);
    }
}
