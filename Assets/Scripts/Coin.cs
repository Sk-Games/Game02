using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Coin : MonoBehaviour {

    [SerializeField] float turnSpeed = 90f;

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null) {
            Destroy(gameObject);    //checking for coin to not overlap at obstacle
            return;
        }

        
        if (other.gameObject.name != "Player") {  //checking for collision with player
            return;
        }

        
        GameManager.inst.IncrementScore(); //adding the score

        // Destroy this coin object
        Destroy(gameObject);
    }


	private void Update () {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);  //rotating the coin
	}
}