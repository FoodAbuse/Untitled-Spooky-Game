using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLogic : MonoBehaviour
{
    [SerializeField]
    private bool colliderTrigger;
    [SerializeField]
    private PlayerSpawnPoint definedPlayerSpawn;
    [SerializeField]
    private bool usesTrigger;
    [SerializeField]
    private bool loadsScene;
    [SerializeField]
    private int sceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        HugoMovementTests p = col.GetComponent<HugoMovementTests>();
        if (col.tag == "Player" && p!=null && colliderTrigger && usesTrigger)
        {
            if(loadsScene)
            {
                SceneManager.LoadScene(sceneIndex);
                Debug.Log("loading scene through door");
            }
            else
                definedPlayerSpawn.TeleportPlayer(col.gameObject);
        }
    }

    public void UseDoor(GameObject player)
    {
        if(loadsScene)
            {
                SceneManager.LoadScene(sceneIndex);
                Debug.Log("loading scene through door");
            }
        else
        definedPlayerSpawn.TeleportPlayer(player);
    }
}
