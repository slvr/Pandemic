using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class City : MonoBehaviour{

    public string aName;
    public int aPopulation;
    public Colour aColor;

    

    public City(string pName, int pPopulation, Colour pColor)
    {
        aName = pName;
        aPopulation = pPopulation;
        aColor = pColor;
    }

    public string GetName()
    {
        return aName;
    }

    public int GetPopulation()
    {
        return aPopulation;
    }

    public Colour GetColor()
    {
        return aColor;
    }

}
