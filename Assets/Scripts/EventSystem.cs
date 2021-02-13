using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    PlayerInput input;

    float tempo;

    // Start is called before the first frame update
    void Start()
    {
      //  setEvents(null, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setEvents(Conductor c, SongMetadata data)
    {
        
    }

    void startEvent(int measure)
    {
        input.SendMessage("setPlayableState");
    }

    void endEvent()
    {
        input.SendMessage("unsetPlayableState");
    }
}
