using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : NetworkBehaviour
{
    void HandleMovement(){
        if(!isLocalPlayer){return;}
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal * .03f, moveVertical * .03f, 0);
        transform.position = transform.position + movement;

    }

    void Update(){
        HandleMovement();
    }
}
