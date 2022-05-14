using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    #region Data

    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image creditsImage;
    [SerializeField] private GameObject creditsButton;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject quitButton;
    private bool inGame = false;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        creditsImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void buttonShowCreditsAction()
    {
        creditsImage.enabled = true;
    }

    public void buttonQuitAction()
    {
        if (inGame)
        {
            playButton.SetActive(true);
            creditsButton.SetActive(true);
            MeshRenderer[] mrPB = playButton.GetComponentsInChildren<MeshRenderer>();
            MeshRenderer[] mrC = creditsButton.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer rend in mrC)
            {
                rend.enabled = true;
            }

            foreach (MeshRenderer rend in mrPB)
            {
                rend.enabled = true;
            }


            inGame = false;
        }
        else
        {
            Application.Quit();
        }
    }

    public void playButtonAction()
    {
        creditsImage.enabled = false;
        MeshRenderer[] mrPB = playButton.GetComponentsInChildren<MeshRenderer>();
        MeshRenderer[] mrC = creditsButton.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer rend in mrC)
        {
            rend.enabled = false;
        }

        foreach (MeshRenderer rend in mrPB)
        {
            rend.enabled = false;
        }

        playButton.SetActive(false);
        creditsButton.SetActive(false);
        inGame = true;
        //screen shows loading
        //buttons dissapear except for quit
        //game starts
    }
}