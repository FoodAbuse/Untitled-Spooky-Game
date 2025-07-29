using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    [SerializeField]
    private bool colliderTrigger;
    [SerializeField]
    private PlayerSpawnPoint definedPlayerSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        HugoMovementTests p = col.GetComponent<HugoMovementTests>();
        if (col.tag == "Player" && p!=null && colliderTrigger)
        {
            definedPlayerSpawn.TeleportPlayer(col.gameObject);
        }
    }
}
