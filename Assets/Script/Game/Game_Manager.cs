using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    [SerializeField] private Text enemykilledText;
    [SerializeField] private int totalEnemy;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject escapePanel;
    [SerializeField] private Transform[] buttons;
    [SerializeField] private GameObject levelOverPanel;

    private int _enemykilled;

    [HideInInspector] public bool canShoot;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        levelOverPanel.SetActive(false);
        canShoot = true;
        _enemykilled = 0;
        enemykilledText.text = _enemykilled.ToString();
    }

    private void Update()
    {
        if (_enemykilled >= totalEnemy)
        {
            door.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void IncrementEnemyKilled()
    {
        _enemykilled++;
        enemykilledText.text = _enemykilled.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.playerTag && _enemykilled == 5)
        {
            FindObjectOfType<Audio_Manager>().Play("LevelCompleted");
            levelOverPanel.SetActive(true);
        }
    }


    public void ResumeButton()
    {

        Time.timeScale = 1;
        FindObjectOfType<Audio_Manager>().Play("Button");
        foreach (var item in buttons)
        {
            item.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        escapePanel.SetActive(false);
    }

    public void HomeButton()
    {
        Time.timeScale = 1;
        FindObjectOfType<Audio_Manager>().Play("Button");
        foreach (var item in buttons)
        {
            item.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        SceneManager.LoadScene(0);
    }

    public void NextButton()
    {
        FindObjectOfType<Audio_Manager>().Play("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
