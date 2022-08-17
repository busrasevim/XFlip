using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance = null;
    public static UIManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }
    #endregion

    [Header("PANELS")]
    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject finishPanel;

    public TextMeshProUGUI playTimeText;
    public TextMeshProUGUI finalText;

    public string isWinText = "CONGRATULATIONS!";
    public string isNotWinText = "TRY AGAIN!";

    // Start is called before the first frame update
    void Start()
    {
        OpenPanel(startPanel, true);
        OpenPanel(gamePanel, false);
        OpenPanel(finishPanel, false);

        LevelManager.Instance.StartAction += StartGame;
        LevelManager.Instance.EndAction += EndGame;

    }

    private void StartGame()
    {
        OpenPanel(startPanel, false);
        OpenPanel(gamePanel, true);
    }

    private void EndGame(bool isWin)
    {
        SetPlayTimeText();
        SetFinalText(isWin);

        OpenPanel(gamePanel, false);
        OpenPanel(finishPanel, true);
    }

    private void OpenPanel(GameObject panel, bool isOpen)
    {
        panel.SetActive(isOpen);

        float alphaValue = 0f;
        if (isOpen) alphaValue = 1f;
        panel.GetComponent<CanvasGroup>().alpha = alphaValue;
    }

    private void SetPlayTimeText()
    {
        playTimeText.text = "PLAY TIME: " + GameManager.Instance.playTime.ToString("0.0") + " s";
    }

    private void SetFinalText(bool isWin)
    {
        if (isWin)
        {
            finalText.text = isWinText;
        }
        else
        {
            finalText.text = isNotWinText;
        }
    }
}
