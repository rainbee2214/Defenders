using UnityEngine;
using System.Collections;

public class PlayerHealthCanvas : MonoBehaviour {

    public Transform player;

    Vector3 pos;

	void FixedUpdate ()
    {
        pos = player.position;
        pos.y += -0.55f;
        transform.position = pos;
	}
}
