using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointBeamController : MonoBehaviour {

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

	public void Spawn()
    {
        anim.SetTrigger("Spawn");
    }

    public void ActivateBeam()
    {
        anim.SetTrigger("Activate");
    }
}
