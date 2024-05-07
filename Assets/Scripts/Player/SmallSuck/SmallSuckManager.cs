using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSuckManager : MonoBehaviour
{
    //När de blir sucked ska man kalla på creatureBehaviour beingSucked
    //Det kommer finnas 3 olika colliders att hålla koll på, och de kommer att ge olika resultat
    // Start is called before the first frame update
    [SerializeField]private SuckArea _smallSuckArea;
    [SerializeField]private SuckArea _middleSuckArea;
    [SerializeField]private SuckArea _bigSuckArea;

    ///Frågor:
    /// Vad händer om man slutar mitt i? Ska man kunna göra det? ja, escape
    /// Måste man hålla in LMB hela tiden eller kan man släppa? nej
    /// Info:
    /// Smååå zoner (som effekten)
    /// prioritera den zon som ger mest poäng
    /// när man går in i minigamet ska small suck flyttas till mitten av skärmen och man ska
    /// se lite mer inzoomat på den.
    private void SuckMiniGame()
    {
        
    }

}
