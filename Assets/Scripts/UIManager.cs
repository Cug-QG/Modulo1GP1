using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else { instance = this; }
    }

    [SerializeField] GameObject pauseMenu;
    [SerializeField] Image beamValueImage;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] Image healthBar;
    [SerializeField] GameObject winningMenu;
    [SerializeField] TextMeshProUGUI messagesText;

    public void TogglePauseMenu(bool isActive) => pauseMenu.SetActive(isActive);
    public void SetBeamValue(float value) => beamValueImage.fillAmount = value;
    public void ToggleGameOverMenu(bool isActive) => gameOverMenu.SetActive(isActive);
    public void SetHealthBarValue(float value) => healthBar.fillAmount = value;
    public void ToggleWinningMenu(bool isActive) => winningMenu.SetActive(isActive);

    public void SetMessagesText(string text) 
    {
        messagesText.text = text;
        StartCoroutine(DeleteMessage());
    }

    IEnumerator DeleteMessage()
    {
        yield return new WaitForSeconds(2);
        messagesText.text = "";
    }
}