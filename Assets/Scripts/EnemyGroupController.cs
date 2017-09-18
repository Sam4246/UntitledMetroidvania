using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupController : MonoBehaviour {

    private EnemyController[] enemies;

	// Use this for initialization
	void Start () {
        enemies = GetComponentsInChildren<EnemyController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Pause()
    {
        for (int i = 0; i < enemies.Length; i++)
            enemies[i].TogglePause();
    }
}
