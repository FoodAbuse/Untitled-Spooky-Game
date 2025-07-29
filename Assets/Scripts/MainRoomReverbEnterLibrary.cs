using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoomReverbEnterLibrary : MonoBehaviour
{
    private bool isInRoom;
    public GameObject ReverbZoneHallway;
    public GameObject ReverbZoneMainRoom;

    private AudioReverbZone hallwayReverb;
    private AudioReverbZone mainRoomReverb;

    void Start()
    {
        isInRoom = false;
        hallwayReverb = ReverbZoneHallway.GetComponent<AudioReverbZone>();
        mainRoomReverb = ReverbZoneMainRoom.GetComponent<AudioReverbZone>();
        //private AudioReverbZone mainRoomReverb = ReverbZoneMainRoom.GetComponent<AudioReverbZone>;
    }

    void OnTriggerEnter()
    {
        hallwayReverb.enabled = false;
        mainRoomReverb.enabled = true;
    }

    void OnTriggerExit()
    {
        hallwayReverb.enabled = true;
        mainRoomReverb.enabled = false;
    }
}
