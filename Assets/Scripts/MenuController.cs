using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Button[] buttons;
    public Image highlightImageLeft;
    public Image highlightImageRight;
    public float offset;

    private int selectedButton = 0;
    private int maxButton;
    private bool moved = false;

    void Start () {

        maxButton = buttons.Length - 1;

        RefreshSelection();
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") > 0.1)
        {
            ScrollUp();
            moved = true;
        }
        else if (Input.GetAxis("Vertical") < -0.1)
        {
            ScrollDown();
            moved = true;
        }
        else
            moved = false;

        if (Input.GetButton("Confirm"))
            SelectMenuItem();
    }

    void RefreshSelection()
    {
        Vector2 highlightPos = buttons[selectedButton].transform.position;
        highlightPos.x -= offset;
        highlightImageLeft.transform.position = highlightPos;

        highlightPos = buttons[selectedButton].transform.position;
        highlightPos.x += offset;
        highlightImageRight.transform.position = highlightPos;
    }

    void ScrollUp()
    {
        if (moved)
            return;

        selectedButton--;
        if (selectedButton < 0)
            selectedButton = maxButton;

        RefreshSelection();
    }

    void ScrollDown()
    {
        if (moved)
            return;

        selectedButton++;
        if (selectedButton > maxButton)
            selectedButton = 0;

        RefreshSelection();
    }

    void SelectMenuItem()
    {
        switch (selectedButton)
        {
            case 0: PlayGame(); break;
            case 1: QuitGame(); break;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Cave World - 0");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
