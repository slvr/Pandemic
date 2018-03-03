using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {

    public string cityName;
    public int population;
    public Colour colour;
}

public enum Colour {
    red,
    blue,
    yellow,
    black
}
