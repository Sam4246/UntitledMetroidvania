using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

    public Image[] hearts;

	void Start () {
		
	}

	void Update () {
        int playerHP = PlayerController.RemainingHP();

        for (int i = 0; i < hearts.Length; i++)
            hearts[i].enabled = true;

        if (playerHP < 5)
            hearts[4].enabled = false;
        if (playerHP < 4)
            hearts[3].enabled = false;
        if (playerHP < 3)
            hearts[2].enabled = false;
        if (playerHP < 2)
            hearts[1].enabled = false;
        if (playerHP < 1)
            hearts[0].enabled = false;
	}
}
