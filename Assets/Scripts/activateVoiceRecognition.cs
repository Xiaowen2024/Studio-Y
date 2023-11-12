using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi;
using Meta.WitAi.Requests;
namespace Oculus.VoiceSDK.UX
{
public class activateVoiceRecognition : MonoBehaviour
{
    // Start is called before the first frame update

    private VoiceService voiceService;

    private VoiceServiceRequest _request;

    private bool isActivated;
    void Start()
    {
        isActivated = false;
        voiceService = FindObjectOfType<VoiceService>();
        activateVoice();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(voiceService.Active);
    }

    public void activateVoice(){
        if (!isActivated)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

   

    public void Activate()
    {
        _request = voiceService.Activate(GetRequestEvents());
        Debug.Log("Activated");

    }

    public void Deactivate()
    {

        _request.DeactivateAudio();
        Debug.Log("Deactivated");
      
    }

    private VoiceServiceRequestEvents GetRequestEvents()
    {
            VoiceServiceRequestEvents events = new VoiceServiceRequestEvents();
            events.OnInit.AddListener(OnInit);
            events.OnComplete.AddListener(OnComplete);
            return events;
    }

    private void OnInit(VoiceServiceRequest request)
        {
            isActivated = true;
            
        }
        // Request completed
        private void OnComplete(VoiceServiceRequest request)
        {
            isActivated = false;
            
        }
    }
}
