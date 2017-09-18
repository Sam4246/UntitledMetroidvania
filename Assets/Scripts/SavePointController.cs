using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointController : MonoBehaviour {

    public bool activated;
    public Sprite savePointOn;
    public Sprite savePointOff;
    public GameObject savePointBeam;
    public float spawnDelayTime;

    private SpriteRenderer sr;
    private bool spawnHere = true;
    private float spawnDelay = 0;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SpawnAtSavePoint();
    }

	void Update () {
        if (activated)
            sr.sprite = savePointOn;
        else
            sr.sprite = savePointOff;

        if (spawnHere)
            spawnDelay += Time.deltaTime;

        if (spawnDelay > spawnDelayTime)
            PlaySpawnBeam();
	}

    void SpawnAtSavePoint()
    {
        spawnHere = true;
        savePointBeam.GetComponent<SavePointBeamController>().ActivateBeam();
    }

    void PlaySpawnBeam()
    {
        savePointBeam.GetComponent<SavePointBeamController>().Spawn();
    }
}
