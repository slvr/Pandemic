using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityHub {

    public int aNumberResearchStations;
    public List<CityNode> aCityNodes;
    public List<City> aCities;

    public CityHub()
    {
        aNumberResearchStations = 0;
        aCities = new List<City>();
        aCityNodes = new List<CityNode>();

     //   GameObject cityNodes = GameObject.Find("CityNodes");
     //   Debug.Log(cityNodes != null);
    }
    public void Start()
    {

    }


    public int GetNumberResearchStations()
    {
        return aNumberResearchStations;
    }

    public void AddCityNode(CityNode pCity)
    {
        aCityNodes.Add(pCity);
    }

    public void AddDiseaseCubes(Colour pColor, CityNode pCity, int num)
    {
        if (aCityNodes.Contains(pCity))
        {
            aCityNodes[aCityNodes.IndexOf(pCity)].AddDisease(pColor, num);
        }
        else
        {
            Debug.Log("List does not contain " + pCity.GetCity().GetName());
        }
    }
    
    public void RemoveDiseaseCubes(Colour pColor, CityNode pCity, int num)
    {
        if (aCityNodes.Contains(pCity))
        {
            aCityNodes[aCityNodes.IndexOf(pCity)].RemoveDisease(pColor, num);
        }
        else
        {
            Debug.Log("List does not contain " + pCity.GetCity().GetName());
        }
    }

    public void MovePawn(CityNode arrivingCity, Pawn pPawn)
    {
        CityNode departingCity = GetCity(pPawn);
        departingCity.RemovePawn(pPawn);
        arrivingCity.AddPawn(pPawn);
    }

    public CityNode GetCity(City pCity)
    {
        foreach(CityNode c in aCityNodes)
        {
            if (c.GetCity() == pCity)
            {
                return c;
            }
        }
        Debug.Log("City not in any Cities. Null returned");
        return null;
    }

    public CityNode GetCity(Pawn pPawn)
    {
        foreach(CityNode aCity in aCityNodes)
        {
            if (aCity.HasPawns())
            {
                foreach(Pawn p in aCity.GetPawns())
                {
                    if (p.Equals(pPawn))
                    {
                        return aCity;
                    }
                }
            }
        }
        Debug.Log("Pawn not in any Cities. Null returned");
        return null;
    }
    

    // TODO Depends on the implementation
    public void InitializeCities()
    {

      //  City test = ScriptableObject.CreateInstance<City>("TestCity", 0, 0); // City("test", 0, 0);
        
       // Debug.Log(test.GetName());
    }


}
