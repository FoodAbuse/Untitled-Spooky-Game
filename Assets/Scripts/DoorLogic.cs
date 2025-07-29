using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    [SerializeField]
    private bool colliderTrigger;
    [SerializeField]
    private PlayerSpawnPoint definedPlayerSpawn;
    [SerializeField]
    private bool usesTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        HugoMovementTests p = col.GetComponent<HugoMovementTests>();
        if (col.tag == "Player" && p!=null && colliderTrigger && usesTrigger)
        {
            definedPlayerSpawn.TeleportPlayer(col.gameObject);
        }
    }

    public void UseDoor(GameObject player)
    {
        definedPlayerSpawn.TeleportPlayer(player);
    }
}
