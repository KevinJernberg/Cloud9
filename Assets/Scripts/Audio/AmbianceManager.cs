using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

// Deklarerar en public enumvariabel som heter Location och som innehåller olika locations ex. Outside, Inside..... Detta sker i global space
public enum Location  
{  
   Ship,
   Weather
}

public class AmbianceManager : MonoBehaviour
{
   // A static variable in Unity is a variable that is shared by all instances of a class. Meaning that, even if there are multiple instances of a script, on many different objects, they will all share the same static variable value
   public static AmbianceManager Instance { get; private set; }//det som hämtas sker bara inom denna class.

   
   //Dekalarering av privata variabler. [SerializedField] gör att de syns i inspectorn
   [Header("Ambiance Emitter")]
   
   [SerializeField]
   private StudioEventEmitter shipAmbianceEmitter;
   [SerializeField]
   private StudioEventEmitter weatherAmbianceEmitter;


   private StudioEventEmitter emitter; // denna emitter är tom och pekar egentligen bara på de andra ambianceemitterna
   private void Awake()
   {
      // kollar om det finns en intance och instance inte är denna instance vill vi förstöra this alltså sig själv, klassen ambiancemanager. Sammantaget gör if och elese statmenteten att det endast tillåter en instance av AmbianceManager att existera
      if (Instance !=null && Instance != this) 
      {
         Destroy(this);//förstör om det skulle finnas en extra AmbianceManager 
      }
      else
      {
         // Om det inte är null och det bara finns en instance så är "this" instancen
         Instance = this;
      }
      DontDestroyOnLoad(this); //Detta gör att instancen inte förstörs då det laddas en ny scen.
   }

   private void Start()
   {
      
   }

   private void Update()
   {
      //Uppdaterar AmbianceManagerns transform position med spelarens transform position. Hittar spelaren genom att leta efter ett globalt Gameobject med "Player"-taggen
      transform.position = GameObject.FindWithTag("Player").transform.position;
   }

   private void GetLocation(Location location)// GetLocation håller reda på vilken emitter som ska spela
   {
      switch (location)
      {
         case Location.Ship:
            emitter = shipAmbianceEmitter;
            break;
         case Location.Weather:
            emitter = weatherAmbianceEmitter;// "emitter" är bara en referensvariabel för andra emitters
            break;
      }
   }

   public void PlayAudio(Location location)
   {
      GetLocation(location); // GetLocation håller reda på vilken emitter som ska spela
      if (!emitter.IsActive) //om emittern inte är igång starta den (boolmetod)
           emitter.Play();
   }

   public void StopAudio(Location location)
   {
      GetLocation(location);
      if (emitter.IsActive) //Inget "!" då emitter ska sluta spela om den är aktiv
          emitter.Stop();
      
   }

   public void SetParameter(Location location, string parameterName, float parameterValue)// skickar med parameterName och parameterValue i metoden räcker inte med location för att parametern ska funka
   {
      GetLocation(location);
      if (emitter.IsActive)
          emitter.SetParameter(parameterName, parameterValue);
   }

   
   
}
