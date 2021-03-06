﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour {

    public Button[] buttons;
    public Image highlightImageLeft;
    public Image highlightImageRight;
    public float offset;

    private GameController gc;
    private int selectedButton = 0;
    private int maxButton;
    private bool moved = false;

    void Start () {
        maxButton = buttons.Length - 1;

        gc.togglePauseMenu();
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

    public void SetGameController(GameController gameController)
    {
        gc = gameController;
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
            case 0: ResumeGame(); break;
            case 1: ExitToMenu(); break;
            case 2: ExitToDesktop(); break;
        }
    }

    public void ResumeGame()
    {
        gc.togglePauseMenu();
        Destroy(gameObject);
    }

    public void ExitToMenu()
    {
        WorldController.SetArea(0);
        WorldController.SetPrevArea(0);
        WorldController.SetPrevWorld(0);
        WorldController.SetSpawn(0);
        WorldController.SetWorld(0);
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }
}
