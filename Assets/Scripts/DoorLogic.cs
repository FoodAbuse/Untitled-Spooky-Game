using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

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
    [SerializeField]
    private CanvasGroup UICanvasGroup;
    [SerializeField]
    private VideoPlayer videoPlayer;

    private bool isVideoPlaying;
    private float videoLength;


    //audio
    public AudioSource doorOpening;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.Pause();
        videoLength = 1f;
    }

    private void OnTriggerEnter(Collider col)
    {

        //GetComponent<Collider>().enabled = false;

        HugoMovementTests p = col.GetComponent<HugoMovementTests>();
        if (col.tag == "Player" && p != null && colliderTrigger && usesTrigger)
        {
            if (loadsScene)
            {
                SceneManager.LoadScene(sceneIndex);
                Debug.Log("loading scene through door");
            }
            else
                definedPlayerSpawn.TeleportPlayer(col.gameObject);
        }

        if (!isVideoPlaying)
        {
            videoPlayer.Play();
            StartCoroutine(UIFadeSequence());
        }
            

    }

    private IEnumerator UIFadeSequence()
    {
        isVideoPlaying = true;

        UICanvasGroup.alpha = 0f;

        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            UICanvasGroup.alpha = Mathf.Clamp01(time / 1f);
            yield return null;
        }

        doorOpening.Play();

        yield return new WaitForSeconds(videoLength);

        //scene loading code here........
        SceneManager.LoadScene(2);
        
        /*
        time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            UICanvasGroup.alpha = Mathf.Clamp01(1f - (time / 1f));
            yield return null;
        }

        UICanvasGroup.alpha = 0f;

        isVideoPlaying = false;
        */
    }

    
    /*
    private void Update()
    {
        if (fadeIn)
        {
            if (UICanvasGroup.alpha < 1)
            {
                UICanvasGroup.alpha += Time.deltaTime;
                if (UICanvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (UICanvasGroup.alpha >= 0)
            {
                UICanvasGroup.alpha -= Time.deltaTime;
                if (UICanvasGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }
    */

    public void UseDoor(GameObject player)
    {
        if (loadsScene)
        {
            SceneManager.LoadScene(sceneIndex);
            Debug.Log("loading scene through door");
        }
        else
            definedPlayerSpawn.TeleportPlayer(player);
    }
}
