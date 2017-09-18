using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour {

    public GameObject healthPrefab;

    public void DropHealth(Vector2 position)
    {
        Instantiate(healthPrefab, position, Quaternion.identity);
    }
}
