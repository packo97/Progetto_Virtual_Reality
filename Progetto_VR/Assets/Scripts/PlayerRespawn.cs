using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawn;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Respawn"))
        {
            respawn=other.GetComponent<Transform>().position;

        }
        Debug.Log(respawn);
    }

    public Vector3 GetRespawn()
    {
        return respawn;
    }

}
