using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{

    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private Transform[] buttons;



    private void Start()
    {
        buttonsPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    public void PlayButton()
    {
        FindObjectOfType<Audio_Manager>().Play("Button");
        foreach (var item in buttons)
        {
            item.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        SceneManager.LoadScene(1);
    }

    public void SettingButton()
    {
        FindObjectOfType<Audio_Manager>().Play("Button");
        foreach (var item in buttons)
        {
            item.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        SettingsPanel.SetActive(true);
        buttonsPanel.SetActive(false);
    }

    public void QuitButton()
    {
        FindObjectOfType<Audio_Manager>().Play("Button");
        foreach (var item in buttons)
        {
            item.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void BackButton()
    {
        FindObjectOfType<Audio_Manager>().Play("Button");
        foreach (var item in buttons)
        {
            item.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        buttonsPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }
}
