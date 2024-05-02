using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceTrigger : MonoBehaviour
{
    public enum Action //En public enum Action som innehåller Start, Stop och SetParameter  
    {
        Start,
        Stop,
        SetParameter
    }
    
    [Serializable]//Detta skrivas för att AudioSettings ska exponeras i inspectorn. 
    public struct AudioSettings
    {
        public Location location; //Denna finns exponerad i global space från AmbianceManager. Structen är ett smidigt sätt för andra script (Ex. AmbianceManager) att hämta ifrån
        public Action action;
        public string parameterName;
        public float parameterValue;
    }
    
    [SerializeField]
    private int counter;//Dekalarerar en privte int som kallas counter
    //Publica audiosettings variabler för TriggerEnter och TriggerExit-lägen
    public AudioSettings[] triggerEnterAudioSettings;
    public AudioSettings[] triggerExitAudioSettings;

    //Skapar en funktion som ska trigga emitters. OnTriggerEnter-funktionen är inbyggd i monobehaviour.
    private void OnTriggerEnter(Collider other)
    {
        //OnTriggerEnter triggas när en annan collider kolliderar med denna. Använder en if sats som jämför med andra "Tags" i detta fall "Player" så att endast "Player" triggar OnTriggerEnter
        if (other.CompareTag("Player"))
        {
            counter++;
            if (counter > 0) //om countern är högre än 0 ska "foreach"loopen för OnTriggerEnter köras
            {
                //går igenom varje AudioSettings i vår array triggerEnterAudioSettings och tillämpar dess unika inställningar.
                foreach (AudioSettings i in triggerEnterAudioSettings)//"i" är namnet för en lokal variabel för varje inställningen i inspektorn som ska utföra något. Den ska kunna utföra flera inställnigar i listan (Array) för varje trigger. "foreach" kan ses som varje "Element" i arrayen
                {
                    switch (i.action)// Använder en switchsats för att jämföra vilken i.action som ska utföras
                    {
                        case Action.Start:
                            AmbianceManager.Instance.PlayAudio(i.location); 
                            break;
                        case Action.Stop:
                            AmbianceManager.Instance.StopAudio(i.location);
                            break;
                        case Action.SetParameter:
                            AmbianceManager.Instance.SetParameter(i.location,i.parameterName,i.parameterValue);
                            break;
                    }
                }
            }
            
        }
    }
    // Se OnTriggerEnter
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            counter--;
            if (counter <= 0)// om countern är mindre eller lika med 0 ska "foreach"loopen för OnTriggerExit köras
            {
                foreach (AudioSettings i in triggerExitAudioSettings)
                {
                   switch (i.action)
                   {
                       case Action.Start:
                           AmbianceManager.Instance.PlayAudio(i.location);// Kör startmetoden som
                           break;
                       case Action.Stop:
                           AmbianceManager.Instance.StopAudio(i.location);//Kör stopmetoden som ligger i AmbianceManager
                           break;
                       case Action.SetParameter:
                           AmbianceManager.Instance.SetParameter(i.location,i.parameterName,i.parameterValue);
                           break;
                   }
                } 
            }
            
        }
    }
}   

