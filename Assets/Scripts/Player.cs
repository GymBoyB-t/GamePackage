using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHolaCountChanged))]
    public int holaCount = 0;

    void HandleMovement(){
        if(!isLocalPlayer){return;}
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal * .03f, moveVertical * .03f, 0);
        transform.position = transform.position + movement;

    }

    void Update(){
        HandleMovement();
        if(isLocalPlayer && Input.GetKeyDown(KeyCode.X)){
             Debug.Log("Sending Hola to the server");
             Hola();
        }

        if(isServer && transform.position.y > 50){
            TooHigh();
        }
    }

    [Command]
    void Hola(){
        Debug.Log("Received Hola from Client!");
        holaCount += 1;
        ReplyHola();
    }

    [TargetRpc]//run on client associated with this object
    void ReplyHola(){
        Debug.Log("Received Hola form Server!");
    }

    [ClientRpc]
    void TooHigh(){
        Debug.Log("Toohigh!");
    }

    void OnHolaCountChanged(int oldCount, int newCount){
        Debug.Log($"We had {oldCount} holas, but now we have {newCount} holas");
    }
}
