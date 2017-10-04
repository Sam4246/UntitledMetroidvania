using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour {

    public float speed;
    public float lifeTime;

    int direction = 1;
    float timer;

	void Start () {
        timer = 0;
	}
	
	void Update () {
        Vector2 pos = transform.position;
        pos.x += (speed * direction * Time.deltaTime);
        transform.position = pos;

        timer += Time.deltaTime;
        if (timer >= lifeTime)
            Destroy(gameObject);
	}

    public void TravelRight(bool right)
    {
        if (right)
        {
            direction = 1;
            Vector3 scale = transform.localScale;
            scale.z = -1;
            transform.localScale = scale;
        }
        else
        {
            direction = -1;
            Vector3 scale = transform.localScale;
            scale.z = 1;
            transform.localScale = scale;
        }
    }
}
